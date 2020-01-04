using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class ValidationService : IValidationService
	{
		public async IAsyncEnumerable<RuleViolation> Validate(object instance)
		{
			await Task.Delay(1000);
			yield return new RuleViolation();
		}
	}
}
