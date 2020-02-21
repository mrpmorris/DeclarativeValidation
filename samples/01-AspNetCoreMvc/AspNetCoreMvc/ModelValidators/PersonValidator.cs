using System.Reflection.Emit;
using AspNetCoreMvc.Models;
using Microsoft.VisualBasic;
using PeterLeslieMorris.DeclarativeValidation;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace AspNetCoreMvc.ModelValidators
{
	public class PersonValidator : AggregateRootValidator<Person>
	{
		public PersonValidator()
		{
			//For(x => x.Address.Country.Name, v => v.NotNull("Must have a name"));

			//For(x => x.Salutation, v => v.NotNull());

			//ForEachValue(x => x.Address.Lines, v =>
			//{
			//	v.NotNull();
			//});

			SwitchForEach(x => x.OtherAddresses, v =>
			{
				v.ForEachValue(x => x.Lines, v => v.NotNull());
			});

			//SwitchForEach(x => x.OtherAddresses, v =>
			//{
			//	v.For(x => x.Area, v => v.NotNull());
			//});

			//When(x => x.FamilyName, @is => @is.NotNull(), c => {
			//	c.For(x => x.GivenName, v => v.NotNull());
			//});

			//SwitchWhen(x => x.Address, @is => @is.NotNull(), address =>
			//{
			//	address.SwitchWhen(x => x.Country, its => its.NotNull(), country =>
			//	{
			//		country.For(x => x.Code, v => v.NotNull("Must have a code"));
			//	});
			//});
		}
	}
}
