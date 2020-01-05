using System;
using System.Collections.Generic;
using System.Linq;

namespace PeterLeslieMorris.DeclarativeValidation.RuleFactories
{
	public class ClassRuleFactory : IRuleFactory
	{
		private readonly List<IRuleFactory> RuleFactories;

		public ClassRuleFactory(IEnumerable<IRuleFactory> ruleFactories)
		{
			RuleFactories = ruleFactories.ToList();
		}

		//TODO: Create a class rule that will process all rules in parallel
		public IRule Create(IServiceProvider serviceProvider)
			=> new CompositeRule(RuleFactories.Select(x => x.Create(serviceProvider)));
	}
}
