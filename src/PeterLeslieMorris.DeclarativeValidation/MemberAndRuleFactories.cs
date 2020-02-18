using System;
using System.Collections.Generic;
using System.Linq;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class MemberAndRuleFactories
	{
		public readonly string MemberPath;
		public readonly Func<object, object> GetMemberValue;
		public readonly IEnumerable<IMemberRuleFactory> RuleFactories;

		public MemberAndRuleFactories(
			string memberPath,
			Func<object, object> getMemberValue,
			IEnumerable<IMemberRuleFactory> ruleFactories)
		{
			if (string.IsNullOrWhiteSpace(memberPath))
				throw new ArgumentNullException(nameof(memberPath));
			if (getMemberValue == null)
				throw new ArgumentNullException(nameof(getMemberValue));
			if (ruleFactories == null)
				throw new ArgumentNullException(nameof(ruleFactories));
			if (!ruleFactories.Any())
				throw new ArgumentException(
					"Must have at least 1 rule factory",
					nameof(ruleFactories));

			MemberPath = memberPath;
			GetMemberValue = getMemberValue;
			RuleFactories = ruleFactories;
		}
	}
}
