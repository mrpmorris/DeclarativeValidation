using System.Collections.Generic;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IValidator
	{
		IAsyncEnumerable<ValidationError> Validate(object source);
	}

	public interface IValidator<TSource> : IValidator
	{
		IAsyncEnumerable<ValidationError> Validator(TSource source);
	}
}
