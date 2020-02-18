using AspNetCoreMvc.Models;
using PeterLeslieMorris.DeclarativeValidation;

namespace AspNetCoreMvc.ModelValidators
{
	public class PersonValidator : RootValidator<Person>
	{
		public PersonValidator()
		{
			For(x => x.Salutation, v => v.NotNull().MinimumLength(2));
			For(x => x.GivenName, v => v.NotNull());
			For(x => x.FamilyName, v => v.NotNull());
			For(x => x.EmailAddress, v => v.NotNull());
			For(x => x.Address.CountryCode, v => v.NotNull());
		}
	}
}
