using System.Collections.Generic;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public delegate IAsyncEnumerable<ValidationError> DoValidationHandler<TSource, TMember>(TSource source, TMember value);
}
