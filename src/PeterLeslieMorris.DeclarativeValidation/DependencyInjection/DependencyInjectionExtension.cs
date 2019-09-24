using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation.DependencyInjection
{
	public static class DependencyInjectionExtension
	{
		public static IServiceCollection AddDeclarativeValidation(
			this IServiceCollection services,
			Assembly assemblyToScan,
			params Assembly[] additionalAssembliesToScan)
		{
			additionalAssembliesToScan =
				additionalAssembliesToScan ?? Array.Empty<Assembly>();

			var allAssemblies =
				new List<Assembly> { assemblyToScan }
				.Union(additionalAssembliesToScan);

			RegisterInjectables(services, allAssemblies);
			IValidator[] validators = DiscoverValidators(allAssemblies);

			services.AddTransient<IValidationService, ValidationService>();

			services.AddSingleton<IValidatorRepository, ValidatorRepository>(sp =>
			{
				var repository = new ValidatorRepository();
				foreach (IValidator validator in validators)
					repository.AddValidator(validator);
				return repository;
			});

			return services;
		}

		private static IValidator[] DiscoverValidators(IEnumerable<Assembly> allAssemblies)
		{
			return allAssemblies
				.SelectMany(x => x.GetExportedTypes())
				.Where(x => !x.IsAbstract)
				.Where(x => typeof(IAggregateRootValidator).IsAssignableFrom(x))
				.Select(x => Activator.CreateInstance(x))
				.Cast<IValidator>()
				.ToArray();
		}

		private static void RegisterInjectables(IServiceCollection services, IEnumerable<Assembly> allAssemblies)
		{
			allAssemblies
				.SelectMany(x => x.GetExportedTypes())
				.Where(x => !x.IsAbstract)
				.Select(x => new
				{
					Type = x,
					InjectableAttribute = x.GetCustomAttribute<InjectableAttribute>()
				})
				.Where(x => x.InjectableAttribute != null)
				.ToList()
				.ForEach(x => x.InjectableAttribute.Register(services, x.Type));
		}
	}
}
