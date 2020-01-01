using PeterLeslieMorris.DeclarativeValidation;
using SimpleMemberValidationSample.Domain;

namespace SimpleMemberValidationSample.ValidationProfiles
{
	class PersonValidation : ValidationProfile
	{
		public PersonValidation()
		{
			ForClass<Person>(x => x
				.ForMember(c => c.HomeAddress, v => v.NotNull())
				.ForMember(c => c.DateOfDeath, v => v.NotNull())
				.ForMember(c => c.Name, v => v.MinLength(3))
				.WhenAll(c => c.Name, m => m.NotNull(), v => v
					.ForMember(c => c.DateOfDeath, v => v.NotNull())
					.ForMember(c => c.HomeAddress.Lines, v => v.MinLength(2))
				)
				.WhenAll(c => c.DateOfDeath, m => m.NotNull(), v => v
					.ForMember(c => c.DateOfBirth, v => v.NotLessThan(c => c.DateOfDeath))
					.ForMember(c => c.Name, v => v.EqualTo(c => c.HomeAddress.Country.Code))
				)
			);
		}
	}
}
