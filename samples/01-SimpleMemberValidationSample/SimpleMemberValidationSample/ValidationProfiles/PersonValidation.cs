using PeterLeslieMorris.DeclarativeValidation;
using SimpleMemberValidationSample.Domain;

namespace SimpleMemberValidationSample.ValidationProfiles
{
	class PersonValidation : ValidationProfile
	{
		public PersonValidation()
		{
			ForClass<Person>(x => x
				.ForMember(x => x.HomeAddress, v => v.NotNull())
				.ForMember(x => x.DateOfDeath, v => v.NotNull())
				.ForMember(x => x.Name, v => v.MinLength(3))
				.When(x => x.Name, m => m.NotNull(), v => v
					.ForMember(x => x.DateOfDeath, v => v.NotNull())
					.ForMember(x => x.HomeAddress.Lines, v => v.MinLength(2))
				)
				.When(x => x.DateOfDeath, m => m.NotNull(), v => v
					.ForMember(x => x.DateOfBirth, v => v.NotLessThan(x => x.DateOfBirth))
				)
			);
		}
	}
}
