using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PeterLeslieMorris.DeclarativeValidation.RuleFactories;

namespace PeterLeslieMorris.DeclarativeValidation.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDeclarativeValidation(
			this IServiceCollection services,
			Assembly assemblyToScan,
			params Assembly[] otherAssembliesToScan)
		{
			otherAssembliesToScan = otherAssembliesToScan ?? Array.Empty<Assembly>();

			IEnumerable<Assembly> assembliesToScan = 
				new List<Assembly> { assemblyToScan, typeof(IRule).Assembly }
				.Union(otherAssembliesToScan);

			assembliesToScan
				.SelectMany(x => x.ExportedTypes)
				.Where(x => !x.IsAbstract)
				.Where(x => typeof(IRule).IsAssignableFrom(x))
				.Select(x => services.AddTransient(x))
				.ToList();

			IEnumerable<KeyValuePair<Type, ClassRuleFactory>> ruleFactories = assembliesToScan
				.SelectMany(x => x.ExportedTypes)
				.Where(x => !x.IsAbstract)
				.Where(x => typeof(ValidationProfile).IsAssignableFrom(x))
				.Select(x => (ValidationProfile)Activator.CreateInstance(x))
				.SelectMany(x => x.CreateRuleFactories())
				.ToList();

			services.AddScoped<IValidationService>(sp => new ValidationService(sp, ruleFactories));

			return services;
		}
	}
}
