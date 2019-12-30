using PeterLeslieMorris.DeclarativeValidation;
using SimpleMemberValidationSample.Domain;

namespace SimpleMemberValidationSample.ValidationProfiles
{
	class PersonValidation : ClassValidationBuilder<Person>
	{
		private string Json;
		public PersonValidation()
		{
			Json = this
				.NotNullOrWhitespace(x => x.Name)
				.MinLength(x => x.Name, 32)
				.NotNull(x => x.HomeAddress.Lines)
				.MinLength(x => x.HomeAddress.Lines, 1)
				.MaxLength(x => x.HomeAddress.Lines, 2)
				.LengthBetween(x => x.HomeAddress.Lines, 1, 2)
				.IsNull(x => x.DateOfDeath)
				.NotNull(x => x.HomeAddress.Country.Code)
				.Build();

		}
	}
}
