using System.Collections.Generic;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IValidationService
	{
		IAsyncEnumerable<RuleViolation> Validate(object instance);
	}
}
