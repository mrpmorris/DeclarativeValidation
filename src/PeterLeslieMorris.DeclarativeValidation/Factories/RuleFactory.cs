using System;

namespace PeterLeslieMorris.DeclarativeValidation.Factories
{
	public class RuleFactory<TRule> : IRuleFactory
		where TRule : Rule
	{
		private readonly Action<TRule> InitializeRuleProperties;

		public RuleFactory(Action<TRule> initializeRuleProperties)
		{
			InitializeRuleProperties = initializeRuleProperties;
		}

		public TRule Create(IServiceProvider serviceProvider, CompositeRule parent)
		{
			var rule = (TRule)serviceProvider.GetService(typeof(TRule));
			InitializeRuleProperties?.Invoke(rule);
			parent.AddRule(rule);
			return rule;
		}

		Rule IRuleFactory.Create(IServiceProvider serviceProvider, CompositeRule parent)
			=> Create(serviceProvider, parent);
	}
}
