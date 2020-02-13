using System;

namespace PeterLeslieMorris.DeclarativeValidation.RuleFactories
{
	public class RuleFactory<TRule> : IRuleFactory
		where TRule : IRule
	{
		public string MemberPath { get; set; }
		private readonly Action<TRule> InitializeRuleProperties;

		public RuleFactory(Action<TRule> initializeRuleProperties)
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
