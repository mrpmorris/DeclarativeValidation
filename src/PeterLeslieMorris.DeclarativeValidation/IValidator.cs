using System;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IValidator
	{
		Type ClassToValidate { get; }
	}

	public interface IValidator<TClass> : IValidator
	{
		Task<bool> ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			TClass obj);
	}
}
