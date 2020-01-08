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
		public readonly object Subject;
		public int PendingValidationsCount { get; private set; }
		public event EventHandler<string> MemberValidationStarted;
		public event EventHandler<string> MemberValidationEnded;
		public event EventHandler AllValidationsEnded;

		private volatile bool IsCompleted;
		private TaskCompletionSource<IEnumerable<RuleViolation>> TaskCompletionSource;
		private readonly ConcurrentDictionary<string, ValidationStatus> OutstandingEvaluations;

		// TODO - Add a task completion source to constrcu
		
		public ValidationContext(object subject)
		{
			Subject = subject;
			OutstandingEvaluations = new ConcurrentDictionary<string, ValidationStatus>();
		}

		public void AddRuleViolation(params RuleViolation[] violations)
		{
			EnsureNotCompleted();
		}

		// TODO - Add ability to specify which members are validated
		internal Task<IEnumerable<RuleViolation>> ValidateAsync(IServiceProvider serviceProvider, IEnumerable<ClassRuleFactory> factories)
		{
			if (TaskCompletionSource == null)
			{
				TaskCompletionSource = new TaskCompletionSource<IEnumerable<RuleViolation>>();

				if (Subject == null || !factories.Any())
				{
					// TODO: This triggers before the consumer can subscribe to any events
					AllValidationsEnded?.Invoke(this, EventArgs.Empty);
					TaskCompletionSource.SetResult(Array.Empty<RuleViolation>());
				}
				else
				{
					foreach (ClassRuleFactory factory in factories)
						factory.Create(serviceProvider).ValidateAsync(this);
				}
			}
			return TaskCompletionSource.Task;
		}

		internal void StartMemberValidation(string memberPath)
		{
			EnsureNotCompleted();

			OutstandingEvaluations.AddOrUpdate(
				key: memberPath,
				addValueFactory: _ =>
				{
					PendingValidationsCount++;
					MemberValidationStarted?.Invoke(this, memberPath);
					return new ValidationStatus(memberPath);
				},
				updateValueFactory: (_, status) => status.StartMemberValidation());
		}

		internal void EndMemberValidation(string memberPath)
		{
			EnsureNotCompleted();

			OutstandingEvaluations.AddOrUpdate(
				key: memberPath,
				addValueFactory: _ => throw new InvalidOperationException(ValidationStatus.ThrowEndMemberValidationWithoutStartMemberValidationExceptionMessage),
				updateValueFactory: (_, status) =>
				{
					status.EndMemberValidation();
					PendingValidationsCount--;
					if (status.PendingValidationsCount == 0)
						MemberValidationEnded?.Invoke(this, memberPath);

					if (PendingValidationsCount == 0)
					{
						var allRuleViolations = OutstandingEvaluations.Values.SelectMany(x => x.GetRuleViolations());
						TaskCompletionSource.SetResult(allRuleViolations);
						AllValidationsEnded?.Invoke(this, EventArgs.Empty);
					}

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
