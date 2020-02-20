using AspNetCoreMvc.Models;
using PeterLeslieMorris.DeclarativeValidation;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace AspNetCoreMvc.ModelValidators
{
	public class PersonValidator : AggregateRootValidator<Person>
	{
		public PersonValidator()
		{
			SwitchWhen(x => x.Address, its => its.NotNull(), address =>
			{
				address.SwitchWhen(x => x.Country, its => its.NotNull(), country =>
				{
					country.For(x => x.Code, v => v.NotNull());
				});
			});
		}
	}
}
