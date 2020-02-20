using System;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IValidator
	{
		Type ClassToValidate { get; }
		Task<bool> ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			object obj);
	}

	internal interface IValidator<TClass> : IValidator
	{
		Task<bool> ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			TClass obj);
	}
}
