using System;

namespace PeterLeslieMorris.DeclarativeValidation.RuleFactories
{
	public class RuleFactory<TRule> : IRuleFactory
		where TRule : Rule
	{
		private readonly Action<TRule> InitializeRuleProperties;

		public RuleFactory(Action<TRule> initializeRuleProperties)
		{
			InitializeRuleProperties = initializeRuleProperties;
		}

		Rule IRuleFactory.Create(IServiceProvider serviceProvider) => Create(serviceProvider);

		public TRule Create(IServiceProvider serviceProvider)
		{
			var rule = (TRule)serviceProvider.GetService(typeof(TRule));
			InitializeRuleProperties?.Invoke(rule);
			return rule;
		}
	}
}
