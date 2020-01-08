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

		IRule IRuleFactory.Create(IServiceProvider serviceProvider) => Create(serviceProvider);

		public TRule Create(IServiceProvider serviceProvider)
		{
			var rule = (TRule)serviceProvider.GetService(typeof(TRule));
			rule.MemberPath = MemberPath;
			InitializeRuleProperties?.Invoke(rule);
			return rule;
		}
	}
}
