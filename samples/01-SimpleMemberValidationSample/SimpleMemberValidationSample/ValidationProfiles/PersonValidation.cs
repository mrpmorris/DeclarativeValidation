using System;
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
				.ForMember(c => c.Name, v => v.MinLength(3).MaxLength(4))
				.WhenAll(c => c.Name, its => its.NotNull(), then => then
					.ForMember(c => c.DateOfDeath, v => v.NotNull())
					.ForMember(c => c.HomeAddress.Lines, v => v.MinLength(2))
				)
				.WhenAll(c => c.DateOfDeath, its => its.NotNull(), then => then
					.ForMember(c => c.DateOfBirth, v => v.NotLessThan(c => c.DateOfDeath))
					.ForMember(c => c.Name, v => v.EqualTo(c => c.HomeAddress.Country.Code))
				)
			);
			ForClass<Person>(x => x
				.ForMember(c => c.DateOfBirth, v => v.GreaterThan(DateTime.Parse("2001-01-01"), errorCode: "A", errorMessageFormat: "B"))
			);
		}
	}
}
