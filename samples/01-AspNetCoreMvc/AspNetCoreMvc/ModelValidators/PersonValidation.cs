using AspNetCoreMvc.Models;
using PeterLeslieMorris.DeclarativeValidation;

namespace AspNetCoreMvc.ModelValidators
{
	public class PersonValidation : ValidationProfile
	{
		public PersonValidation()
		{
			ForClass<Person>(x => x
				.ForMember(c => c.Salutation, its => its.NotNull().MinLength(2))
				.ForMember(c => c.FamilyName, its => its.NotNull().MinLength(1))
				.WhenAll(c => c.FamilyName, its => its.GreaterThan("x"), then => then
					.ForMember(c => c.GivenName, its => its.NotNull())
				)
			);
		}
	}
}
