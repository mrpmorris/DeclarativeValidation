using AspNetCoreMvc.Models;
using PeterLeslieMorris.DeclarativeValidation;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace AspNetCoreMvc.ModelValidators
{
	public class PersonValidator : AggregateRootValidator<Person>
	{
		public PersonValidator()
		{
			For(x => x.Address.Lines, v => v.IsNotNull().HasMinLength(2).HasMaxLength(3, getErrorMessage: value => $"Max length 2 but found {value.Length}."));
		}
	}
}
