using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Rules
{
	public class MaxLengthRule : RuleBase
	{
		public ulong Max { get; set; }

		public MaxLengthRule()
			: base(
					errorCode: "MaxLength",
					errorMessageFormat: "Maximum length {0}")
		{
		}

		public override string GetErrorMessage() => string.Format(ErrorMessageFormat, Max);

		public override string ToJson()
		{
			return "";
		}

		public override Task ValidateAsync(ValidationContext context)
		{
			return Task.CompletedTask;
		}
	}
}
