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
			For(x => x.Address.Lines, lines => lines
				.IsNotNull()
				.HasMinLength(2, getErrorMessage: _ => $"At least 2 lines are required")
				.HasMaxLength(3, getErrorMessage: value => $"At most 3 lines are allowed, but found {value.Length}."));
			ForEachValue(x => x.OtherAddresses, it => it.IsNotNull());
		}
	}
}
