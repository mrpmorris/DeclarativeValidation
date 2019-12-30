using PeterLeslieMorris.DeclarativeValidation;
using SimpleMemberValidationSample.Domain;

namespace SimpleMemberValidationSample.ValidationProfiles
{
	class PersonValidation : ClassValidationBuilder<Person>
	{
		private string Json;
		public PersonValidation()
		{
			this
				.NotNullOrEmpty(x => x.Name);

			Json = this
				.IsNull(x => x.DateOfDeath)
				.NotNull(x => x.HomeAddress.Country.Code)
				.Build();

		}
	}
}
