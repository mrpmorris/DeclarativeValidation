using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class AddDeclarativeValidationExtension
	{
		public static IServiceCollection AddDeclarativeValidation(
			this IServiceCollection services,
			Assembly assemblyToScan,
			params Assembly[] additionalAssembliesToScan)
		{
			var allAssembliesToScan = new List<Assembly> { assemblyToScan };
			if (additionalAssembliesToScan != null)
				allAssembliesToScan.AddRange(additionalAssembliesToScan);

			IEnumerable<IClassValidator> validators = allAssembliesToScan
				.SelectMany(x => x.GetExportedTypes())
				.Where(x => x.IsClass)
				.Where(x => !x.IsAbstract)
				.Where(x => typeof(IClassValidator).IsAssignableFrom(x))
				.Select(x => Activator.CreateInstance(x))
				.Cast<IClassValidator>();

			var validatorRepository = new ValidatorRepository();
			validatorRepository.AddClassValidators(validators);
			services.AddSingleton<IValidatorRepository>(validatorRepository);

			RegisterRuleTypes(services, allAssembliesToScan);
			return services;
		}

		private static void RegisterRuleTypes(IServiceCollection services, List<Assembly> allAssembliesToScan)
		{
			IEnumerable<Type> ruleTypes = allAssembliesToScan
				.Union(Enumerable.Repeat(typeof(IRule).Assembly, 1))
				.SelectMany(x => x.GetExportedTypes())
				.Where(x => x.IsClass)
				.Where(x => !x.IsAbstract)
				.Where(x => typeof(IRule).IsAssignableFrom(x));
			foreach (Type ruleType in ruleTypes)
				services.AddTransient(ruleType);
		}
	}
}
