using AspNetCoreMvc.Models;
using AspNetCoreMvc.RuleEvaluators.PersonRuleEvaluators;
using PeterLeslieMorris.DeclarativeValidation;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace AspNetCoreMvc.ModelValidators
{
	public class PersonValidator : AggregateRootValidator<Person>
	{
		public PersonValidator()
		{
			For(c => c.EmailAddress, it => it.IsNotNull().IsValidInternetEmailAddress());
			When(c => c.EmailAddress, it => it.IsNotNull(), it =>
			{
				it.IsAUniqueEmailAddress(x => $"\"{x}\" is not a unique email address");
			});
			For(x => x.Address.Lines, line => line
				.IsNotNull()
				.HasMinLength(2)
				.HasMaxLength(3, getErrorMessage: value => $"Max length 3 but found {value.Length}."));
		}
	}
}
