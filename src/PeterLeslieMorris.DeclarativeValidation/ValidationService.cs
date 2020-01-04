using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.RuleFactories;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class ValidationService : IValidationService
	{
		private Dictionary<Type, IEnumerable<ClassRuleFactory>> RuleFactoriesByClass;

		public ValidationService(IEnumerable<KeyValuePair<Type, ClassRuleFactory>> ruleFactories)
		{
			if (ruleFactories == null)
				throw new ArgumentNullException(nameof(ruleFactories));

			RuleFactoriesByClass = ruleFactories
				.GroupBy(x => x.Key)
				.ToDictionary(x => x.Key, _ => _.Select(x => x.Value));
		}

		public async IAsyncEnumerable<RuleViolation> Validate(object instance)
		{
			await Task.Delay(1000);
			yield return new RuleViolation();
		}

	}
}
