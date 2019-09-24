using System;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IValidationService
	{
		Task<ValidationContext> ValidateAsync<TAggregateRoot>(TAggregateRoot obj);
		Task<ValidationContext> ValidateAsync(object obj, ValidationContext context);
	}

	public class ValidationService : IValidationService
	{
		private readonly IServiceProvider ServiceProvider;
		private readonly IValidatorRepository ValidatorRepository;

		public ValidationService(
			IServiceProvider serviceProvider,
			IValidatorRepository validatorRepository)
		{
			ServiceProvider = serviceProvider;
			ValidatorRepository = validatorRepository;
		}

		public Task<ValidationContext> ValidateAsync<TAggregateRoot>(TAggregateRoot obj) =>
			ValidateAsync(
				obj: obj,
				context: new ValidationContext(scenario: null, memberPaths: null));

		public async Task<ValidationContext> ValidateAsync(object obj, ValidationContext context)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));
			if (obj == null)
				throw new ArgumentNullException(nameof(obj));

			Type classToValidate = obj.GetType();
			foreach (IValidator validator in ValidatorRepository.GetValidators(classToValidate))
				await validator.ValidateAsync(
					ServiceProvider,
					context,
					memberPathSoFar: Array.Empty<string>(),
					obj: obj);

			return context;
		}
	}
}
