using PeterLeslieMorris.DeclarativeValidation;
using SimpleMemberValidationSample.Domain;

namespace SimpleMemberValidationSample.Validators
{
	public class PersonValidator : ClassValidationFor<Person>
	{
		public PersonValidator()
		{
			MemberRuleFor(x => x.Name);
		}
	}
}
