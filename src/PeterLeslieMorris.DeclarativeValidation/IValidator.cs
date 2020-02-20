using System;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	internal interface IValidator<TClass>
	{
		Task ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			TClass obj);
	}
}
