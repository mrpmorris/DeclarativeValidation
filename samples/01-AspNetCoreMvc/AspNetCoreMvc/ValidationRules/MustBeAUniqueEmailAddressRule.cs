using System.Threading.Tasks;
using AspNetCoreMvc.Models;
using AspNetCoreMvc.Services;
using PeterLeslieMorris.DeclarativeValidation;
using PeterLeslieMorris.DeclarativeValidation.RuleBuilders;
using PeterLeslieMorris.DeclarativeValidation.RuleFactories;

namespace AspNetCoreMvc.ValidationRules
{
	public class MustBeAUniqueEmailAddressRule : RuleBase
	{
		private readonly IPersonRepository PersonRepository;

		public MustBeAUniqueEmailAddressRule(IPersonRepository personRepository)
			: base(
					errorCode: "MustBeAUniqueEmailAddress",
					errorMessageFormat: "Must be a unique email address")
		{ 
			PersonRepository = personRepository;
		}

		public override string GetErrorMessage() => ErrorMessageFormat;
		public override string ToJson() => "";

		public async override Task ValidateAsync(ValidationContext context)
		{
			await Task.Delay(1000);
			context.AddRuleViolation(CreateRuleViolation());
		}
	}

	public static class MustBeAUniqueEmailAddressRuleExtension
	{
		public static MemberRuleBuilder<Person, string> MustBeAUniqueEmailAddress(
			this MemberRuleBuilder<Person, string> builder,
			string errorCode = null,
			string errorMessageFormat = null)
		{
			var factory = new RuleFactory<MustBeAUniqueEmailAddressRule>(x => {
				x.ErrorCode = errorCode ?? x.ErrorCode;
				x.ErrorMessageFormat = errorMessageFormat ?? x.ErrorMessageFormat;
			});
			builder.AddRuleFactory(factory);
			return builder;
		}
		
	}
}
