using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PeterLeslieMorris.DeclarativeValidation.Builders;
using PeterLeslieMorris.DeclarativeValidation.Factories;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class ValidationProfile
	{
		private readonly List<IClassRuleBuilder> RuleBuilders = new List<IClassRuleBuilder>();

		public void ForClass<TClass>(Action<IClassRuleBuilder<TClass>> validation)
			where TClass : class
		{
			var ruleBuilder = new ClassRuleBuilder<TClass>();
			RuleBuilders.Add(ruleBuilder);
			validation(ruleBuilder);
		}

		//public IEnumerable<KeyValuePair<Type, ClassRuleFactory>> CreateRuleFactories()
		//	=> RuleBuilders.Select(x => new KeyValuePair<Type, ClassRuleFactory>(x.ClassType, x.CreateRuleFactory()));
	}
}
