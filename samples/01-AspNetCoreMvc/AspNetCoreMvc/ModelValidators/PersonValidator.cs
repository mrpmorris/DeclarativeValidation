using AspNetCoreMvc.Models;
using PeterLeslieMorris.DeclarativeValidation;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace AspNetCoreMvc.ModelValidators
{
	public class PersonValidator : AggregateRootValidator<Person>
	{
		public PersonValidator()
		{
			For(x => x.Address.Lines, v => v.IsNotNull().HasMinLength(4).HasMaxLength(3));
		}
	}
}
