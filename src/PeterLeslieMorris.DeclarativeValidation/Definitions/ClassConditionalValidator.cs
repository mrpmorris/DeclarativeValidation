using System;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public class ClassConditionalValidator<TClass, TMember> : ClassValidator<TClass>, IValidator<TClass>
	{
		Task IValidator<TClass>.ValidateAsync(IServiceProvider serviceProvider, IValidationContext context, TClass obj)
		{
			throw new NotImplementedException();
		}
	}
}
