using System;
using System.Collections.Generic;
using System.Linq;
using PeterLeslieMorris.DeclarativeValidation.Builders;

namespace PeterLeslieMorris.DeclarativeValidation.Factories
{
	public class ClassRuleFactory : IRuleFactory
	{
		private readonly List<IRuleFactory> RuleFactories;

		public ClassRuleFactory(IEnumerable<IRuleFactory> ruleFactories)
		{
			RuleFactories = ruleFactories.ToList();
		}

		public Rule Create(IServiceProvider serviceProvider, CompositeRule parent)
		{
			throw new NotImplementedException();
			//var result = new CompositeRule();
			//foreach (IRuleBuilder ruleBuilder in RuleBuilders)
			//{
			//	ruleBuilder.
			//}
		}
	}
}
