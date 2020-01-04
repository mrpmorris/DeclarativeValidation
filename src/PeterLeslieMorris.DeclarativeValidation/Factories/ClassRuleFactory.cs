using System;
using System.Collections.Generic;
using System.Linq;

namespace PeterLeslieMorris.DeclarativeValidation.Factories
{
	public class ClassRuleFactory : IRuleFactory
	{
		private readonly List<IRuleFactory> RuleFactories;

		public ClassRuleFactory(IEnumerable<IRuleFactory> ruleFactories)
		{
			RuleFactories = ruleFactories.ToList();
		}

		public Rule Create(IServiceProvider serviceProvider)
			=> new CompositeRule(RuleFactories.Select(x => x.Create(serviceProvider)));
	}
}
