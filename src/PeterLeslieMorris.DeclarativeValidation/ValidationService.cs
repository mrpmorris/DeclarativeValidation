using System;
using System.Collections.Generic;
using System.Linq;
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

		public ValidationContext Validate(object subject)
		{
			var context = new ValidationContext(subject);
			Validate(context);
			return context;
		}

		public void Validate(ValidationContext context)
		{
			if (context == null)
				throw new NotImplementedException(nameof(context));

			object subject = context.Subject;


			// TODO: Walk the type hierarchy

			if (!RuleFactoriesByClass.TryGetValue(subject.GetType(), out IEnumerable<ClassRuleFactory> factories))
				factories = Array.Empty<ClassRuleFactory>();

			context.Validate(ServiceProvider, factories);
		}
	}
}
