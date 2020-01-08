using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Rules
{
	public class MinLengthRule : RuleBase
	{
		public ulong Min { get; set; }

		public MinLengthRule()
			: base(
					errorCode: "MinLength",
					errorMessageFormat: "Minimum length {0}")
		{
		}

		public override string GetErrorMessage() => string.Format(ErrorMessageFormat, Min);

		public override string ToJson()
		{
			return "";
		}

		public override Task ValidateAsync(ValidationContext context)
		{
			context.AddRuleViolation(CreateRuleViolation());
			return Task.CompletedTask;
		}
	}
}
