using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.RuleFactories;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class ValidationService : IValidationService
	{
		private readonly IServiceProvider ServiceProvider;
		private Dictionary<Type, IEnumerable<ClassRuleFactory>> RuleFactoriesByClass;

		public ValidationService(IServiceProvider serviceProvider, IEnumerable<KeyValuePair<Type, ClassRuleFactory>> ruleFactories)
		{
			if (ruleFactories == null)
				throw new ArgumentNullException(nameof(ruleFactories));

			RuleFactoriesByClass = ruleFactories
				.GroupBy(x => x.Key)
				.ToDictionary(x => x.Key, _ => _.Select(x => x.Value));
			ServiceProvider = serviceProvider;
		}

		public async IAsyncEnumerable<RuleViolation> Validate(object instance)
		{
			if (instance == null)
				yield break;

			// TODO: Walk the hierarchy

			if (!RuleFactoriesByClass.TryGetValue(instance.GetType(), out IEnumerable<ClassRuleFactory> factories))
				yield break;

			foreach(ClassRuleFactory classRuleFactory in factories)
			{
				Rule rule = classRuleFactory.Create(ServiceProvider);
				await foreach(RuleViolation ruleViolation in rule.Validate(instance))
				{
					yield return ruleViolation;
				}
			}
		}

	}
}
