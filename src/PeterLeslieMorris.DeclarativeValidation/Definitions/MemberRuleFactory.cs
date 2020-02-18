using System;
using Microsoft.Extensions.DependencyInjection;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public interface IMemberRuleFactory
	{
		public IRule CreateRule(IServiceProvider serviceProvider);
	}

	public class MemberRuleFactory<TMemberRule> : IMemberRuleFactory
		where TMemberRule: IRule
	{
		internal readonly Action<TMemberRule> InitializeRule;

		public MemberRuleFactory(Action<TMemberRule> initializeRule)
		{
			InitializeRule = initializeRule;
		}

		public IRule CreateRule(IServiceProvider serviceProvider)
		{
			var rule = serviceProvider.GetRequiredService<TMemberRule>();
			InitializeRule(rule);
			return rule;
		}
	}
}
