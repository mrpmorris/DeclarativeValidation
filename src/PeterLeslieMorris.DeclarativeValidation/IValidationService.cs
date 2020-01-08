using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IValidationService
	{
		Task<IEnumerable<RuleViolation>> ValidateAsync(object subject);
		//TODO: Make ValidateAsync
		Task<IEnumerable<RuleViolation>> ValidateAsync(ValidationContext context);
	}
}
