using System;

namespace PeterLeslieMorris.DeclarativeValidation.RuleFactories
{
	public class MemberRuleFactory<TRule> : IRuleFactory
		where TRule : IRule
	{
		public string MemberPath { get; set; }
		private readonly Action<TRule> InitializeRuleProperties;

		public MemberRuleFactory(Action<TRule> initializeRuleProperties)
		{
			InitializeRuleProperties = initializeRuleProperties;
		}

		public IRule Create(IServiceProvider serviceProvider)
		{
			var rule = (TRule)serviceProvider.GetService(typeof(TRule));
			InitializeRuleProperties?.Invoke(rule);
			return rule;
		}
	}
}
