using PeterLeslieMorris.DeclarativeValidation;
using SimpleMemberValidationSample.Domain;

namespace SimpleMemberValidationSample.ValidationProfiles
{
	class PersonValidation : ValidationProfile
	{
		public PersonValidation()
		{
			ForClass<Person>(x => x
				.ForMember(c => c.HomeAddress, p => p
					.NotNull())
				.ForMember(c => c.DateOfDeath, p => p
					.NotNull())
			);
		}
	}
}
