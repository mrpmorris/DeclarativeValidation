using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.RuleFactories;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public sealed class ValidationContext
	{
		public int PendingValidationsCount { get; private set; }
		public event EventHandler<string> MemberValidationStarted;
		public event EventHandler<string> MemberValidationEnded;
		public event EventHandler AllValidationsEnded;

		private volatile bool IsCompleted;
		private readonly ConcurrentDictionary<string, ValidationStatus> RuleEvaluations;

		// TODO - Add a task completion source to constrcu

		public ValidationContext()
		{
			RuleEvaluations = new ConcurrentDictionary<string, ValidationStatus>();
		}

		public void AddRuleViolation(params RuleViolation[] ruleViolations)
		{
			if (ruleViolations is null)
				throw new ArgumentNullException(nameof(ruleViolations));

			EnsureNotCompleted();
			foreach (RuleViolation ruleViolation in ruleViolations)
				RuleEvaluations.AddOrUpdate(
					key: ruleViolation.MemberPath ?? "",
					addValueFactory: _ =>
					{
						var result = new ValidationStatus(ruleViolation.MemberPath);
						result.AddRuleViolation(ruleViolation);
						return result;
					},
					updateValueFactory: (_, status) =>
					{
						status.AddRuleViolation(ruleViolation);
						return status;
					});
		}

		// TODO - Add ability to specify which members are validated
		internal async Task<IEnumerable<RuleViolation>> ValidateAsync(
			IServiceProvider serviceProvider,
			IEnumerable<ClassRuleFactory> ruleFactories,
			object value)
		{
			if (serviceProvider is null)
				throw new ArgumentNullException(nameof(serviceProvider));
			if (ruleFactories == null)
				throw new ArgumentNullException(nameof(ruleFactories));

			var tasks = ruleFactories
				.Select(x => x.Create(serviceProvider).ValidateAsync(this, value));
			await Task.WhenAll(tasks);

			AllValidationsEnded?.Invoke(this, EventArgs.Empty);
			return RuleEvaluations.Values.SelectMany(x => x.GetRuleViolations());
		}

		internal void StartMemberValidation(string memberPath)
		{
			EnsureNotCompleted();

			bool memberAdded = false;
			RuleEvaluations.AddOrUpdate(
				key: memberPath ?? "",
				addValueFactory: _ =>
				{
					memberAdded = true;
					PendingValidationsCount++;
					var result = new ValidationStatus(memberPath);
					result.StartMemberValidation();
					return result;
				},
				updateValueFactory: (_, status) => status.StartMemberValidation());

			if (memberAdded)
				MemberValidationStarted?.Invoke(this, memberPath);
		}

		internal void EndMemberValidation(string memberPath)
		{
			EnsureNotCompleted();

			RuleEvaluations.AddOrUpdate(
				key: memberPath ?? "",
				addValueFactory: _ => throw new InvalidOperationException(ValidationStatus.ThrowEndMemberValidationWithoutStartMemberValidationExceptionMessage),
				updateValueFactory: (_, status) =>
				{
					status.EndMemberValidation();
					PendingValidationsCount--;
					if (status.PendingValidationsCount == 0)
						MemberValidationEnded?.Invoke(this, memberPath);

					return status;
				});
		}

		private void EnsureNotCompleted()
		{
			if (IsCompleted)
				throw new InvalidOperationException("Context has completed");
		}
	}
}
