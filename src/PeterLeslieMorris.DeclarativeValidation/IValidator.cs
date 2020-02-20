using System;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	internal interface IValidator<TClass>
	{
		Task<bool> ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			TClass obj);
	}
}
