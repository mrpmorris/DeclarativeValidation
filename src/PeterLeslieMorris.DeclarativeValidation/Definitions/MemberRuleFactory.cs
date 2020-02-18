using System;
using Microsoft.Extensions.DependencyInjection;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public class MemberRuleFactory<TMemberRule>
	{
		internal readonly Action<TMemberRule> InitializeRule;

		public MemberRuleFactory(Action<TMemberRule> initializeRule)
		{
			InitializeRule = initializeRule;
		}

		public TMemberRule CreateRule(IServiceProvider serviceProvider)
		{
			var rule = serviceProvider.GetRequiredService<TMemberRule>();
			InitializeRule(rule);
			return rule;
		}
	}
}
