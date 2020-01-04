using System;
using System.Collections.Generic;
using System.Linq;
using PeterLeslieMorris.DeclarativeValidation.RuleBuilders;
using PeterLeslieMorris.DeclarativeValidation.Factories;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class ValidationProfile
	{
		private readonly List<ClassRuleBuilder> ClassRuleBuilders = new List<ClassRuleBuilder>();

		public void ForClass<TClass>(Action<ClassRuleBuilder<TClass>> validation)
			where TClass : class
		{
			var ruleBuilder = new ClassRuleBuilder<TClass>();
			ClassRuleBuilders.Add(ruleBuilder);
			validation(ruleBuilder);
		}

		internal IEnumerable<KeyValuePair<Type, ClassRuleFactory>> CreateRuleFactories()
			=> ClassRuleBuilders.Select(x => new KeyValuePair<Type, ClassRuleFactory>(x.ClassType, x.CreateRuleFactory()));
	}
}
