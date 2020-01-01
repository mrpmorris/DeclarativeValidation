using PeterLeslieMorris.DeclarativeValidation;
using SimpleMemberValidationSample.Domain;

namespace SimpleMemberValidationSample.ValidationProfiles
{
	class PersonValidation : ValidationProfile
	{
		public PersonValidation()
		{
			ForClass<Person>(_ => _
				.ForMember(x => x.HomeAddress, v => v.NotNull())
				.ForMember(x => x.DateOfDeath, v => v.NotNull())
				.ForMember(x => x.Name, v => v.MinLength(3))
				.When(x => x.Name, c => c.NotNull(), v => v
					.ForMember(x => x.DateOfDeath, v => v.NotNull())
					.ForMember(x => x.HomeAddress.Lines, v => v.MinLength(2))
				)
			);
		}
	}
}
