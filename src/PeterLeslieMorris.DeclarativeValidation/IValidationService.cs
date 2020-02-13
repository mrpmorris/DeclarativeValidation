using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IValidationService
	{
		Task<IEnumerable<RuleViolation>> ValidateAsync(ValidationContext context, object subject);
	}
}
