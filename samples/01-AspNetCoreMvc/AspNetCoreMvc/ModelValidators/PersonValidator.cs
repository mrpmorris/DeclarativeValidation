using AspNetCoreMvc.Models;
using PeterLeslieMorris.DeclarativeValidation;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace AspNetCoreMvc.ModelValidators
{
	public class PersonValidator : AggregateRootValidator<Person>
	{
		public PersonValidator()
		{
			When(x => x.Address, its => its.NotNull(), address =>
			{
				address.When(x => x.Country, its => its.NotNull(), country =>
				{
					country.For(x => x.Code, v => v.NotNull());
				});
			});
			For(x => x.Address.Country.Code, v => v
			.NotNull(errorCode: "NOTNULL", errorMessage: "Put one in")
			);
		}
	}
}
