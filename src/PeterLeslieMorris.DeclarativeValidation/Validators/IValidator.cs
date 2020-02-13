using System;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.RuleFactories
{
	public interface IValidator
	{
		Task ValidateAsync(IServiceProvider serviceProvider, object value);
	}

	public interface IValidator<T>
	{
		Task ValidateAsync(IServiceProvider serviceProvider, T value);
	}
}
