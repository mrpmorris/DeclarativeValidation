using AspNetCoreMvc.Models;
using PeterLeslieMorris.DeclarativeValidation;
using AspNetCoreMvc.ValidationRules;

namespace AspNetCoreMvc.ModelValidators
{
	public class PersonValidation : ValidationProfile
	{
		public PersonValidation()
		{
			ForClass<Person>(x => x
				.ForMember(c => c.Salutation, v => v.NotNull().MinLength(2))
				.ForMember(c => c.FamilyName, v => v.NotNull().MinLength(1))
				.ForMember(c => c.EmailAddress, v => v.MustBeAUniqueEmailAddress())
				.WhenAll(c => c.FamilyName, m => m.GreaterThan("x"), then => then
					.ForMember(c => c.GivenName, v => v.NotNull())
				)
			);
		}
	}
}
