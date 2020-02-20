using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
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

			var validators = allAssemblies
				.SelectMany(x => x.GetExportedTypes())
				.Where(x => !x.IsAbstract)
				.Where(x => typeof(IAggregateRootValidator).IsAssignableFrom(x))
				.Select(x => Activator.CreateInstance(x))
				.Cast<IValidator>()
				.ToArray();

			services.AddSingleton<IValidatorRepository, ValidatorRepository>(sp =>
			{
				var repository = new ValidatorRepository();
				foreach (IValidator validator in validators)
					repository.AddValidator(validator);
				return repository;
			});

			return services;
		}
	}
}
