using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
		private readonly ConcurrentDictionary<string, ValidationStatus> OutstandingEvaluations;

		// TODO - Add a task completion source to constrcu
		
		internal ValidationContext(object subject)
		{
			Subject = subject ?? throw new ArgumentNullException(nameof(subject));
			OutstandingEvaluations = new ConcurrentDictionary<string, ValidationStatus>();
		}

		public void AddRuleViolations(string memberPath, IEnumerable<RuleViolation> violations)
		{
			EnsureNotCompleted();
		}

		internal void Validate(IServiceProvider serviceProvider, IEnumerable<ClassRuleFactory> factories)
		{
			// TODO - Add ability to specify which members are validated
			foreach (ClassRuleFactory factory in factories)
				factory.Create(serviceProvider).Validate(Subject);
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
						AllValidationsEnded?.Invoke(this, EventArgs.Empty);

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
