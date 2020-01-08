using System;
using System.Collections.Generic;
using System.Linq;

namespace PeterLeslieMorris.DeclarativeValidation.RuleFactories
{
	public class ClassRuleFactory : IRuleFactory
	{
		public string MemberPath { get; set; }
		private readonly List<IRuleFactory> RuleFactories;

		public ClassRuleFactory(IEnumerable<IRuleFactory> ruleFactories)
		{
			RuleFactories = ruleFactories.ToList();
		}

		public IRule Create(IServiceProvider serviceProvider)
			=> new CompositeParallelRule(RuleFactories.Select(x => x.Create(serviceProvider)));
	}
}
