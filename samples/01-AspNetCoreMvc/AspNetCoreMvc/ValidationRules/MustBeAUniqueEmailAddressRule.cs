using System.Threading.Tasks;
using AspNetCoreMvc.Models;
using AspNetCoreMvc.Services;
using PeterLeslieMorris.DeclarativeValidation;
using PeterLeslieMorris.DeclarativeValidation.RuleBuilders;
using PeterLeslieMorris.DeclarativeValidation.RuleFactories;

namespace AspNetCoreMvc.ValidationRules
{
	public class MustBeAUniqueEmailAddressRule : IRule
	{
		private readonly IPersonRepository PersonRepository;

		public MustBeAUniqueEmailAddressRule(IPersonRepository personRepository)
		{ 
			PersonRepository = personRepository;
		}

		public async Task<bool> ValidateAsync(ValidationContext context, object value)
		{
			await Task.Delay(1000);
			return false;
		}
	}

	public static class MustBeAUniqueEmailAddressRuleExtension
	{
		public static MemberRuleBuilder<Person, string> MustBeAUniqueEmailAddress(
			this MemberRuleBuilder<Person, string> builder,
			string errorCode = null,
			string errorMessageFormat = null)
		{
			var factory = new MemberRuleFactory<MustBeAUniqueEmailAddressRule>(null);
			builder.AddRuleFactory(factory);
			return builder;
		}
		
	}
}
