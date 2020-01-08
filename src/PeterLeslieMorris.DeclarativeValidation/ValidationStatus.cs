using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class ValidationStatus
	{
		public readonly string MemberPath;
		public int PendingValidationsCount { get; private set; }

		internal static string ThrowEndMemberValidationWithoutStartMemberValidationExceptionMessage 
			= $"{nameof(EndMemberValidation)} called without matching {nameof(StartMemberValidation)}";
		private readonly ConcurrentQueue<RuleViolation> RuleViolations;

		public ValidationStatus(string memberPath)
		{
			MemberPath = memberPath;
			PendingValidationsCount = 0;
			RuleViolations = new ConcurrentQueue<RuleViolation>();
		}

		public IEnumerable<RuleViolation> GetRuleViolations() => RuleViolations;
		public bool Completed => PendingValidationsCount == 0;

		internal void AddRuleViolation(RuleViolation ruleViolation)
		{
			RuleViolations.Enqueue(ruleViolation);
		}

		internal ValidationStatus StartMemberValidation()
		{
			PendingValidationsCount += 1;
			return this;
		}

		internal ValidationStatus EndMemberValidation()
		{
			PendingValidationsCount -= 1;
			if (PendingValidationsCount < 0)
				throw new InvalidOperationException(ThrowEndMemberValidationWithoutStartMemberValidationExceptionMessage);
			return this;
		}
	}
}
