using AspNetCoreMvc.Models;
using PeterLeslieMorris.DeclarativeValidation;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace AspNetCoreMvc.ModelValidators
{
	public class PersonValidator : AggregateRootValidator<Person>
	{
		public PersonValidator()
		{
			For(x => x.Address.Country.Code, v => v
			.NotNull(errorCode: "NOTNULL", errorMessage: "Put one in")
			);
		}
	}
}
