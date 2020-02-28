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
			For(c => c.EmailAddress, v => v.IsNotNull());
			When(c => c.EmailAddress, it => it.IsNotNull(), v =>
			{
				v.HasUniqueEmailAddress(x => $"\"{x}\" is not a valid email address");
			});
			For(x => x.Address.Lines, v => v.IsNotNull().HasMinLength(2).HasMaxLength(3, getErrorMessage: value => $"Max length 2 but found {value.Length}."));
		}
	}
}
