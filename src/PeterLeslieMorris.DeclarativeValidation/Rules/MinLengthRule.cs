namespace PeterLeslieMorris.DeclarativeValidation.Rules
{
	public class MinLengthRule : MemberRule
	{
		public readonly ulong Min;

		public MinLengthRule(ulong min, string errorCode, string errorMessageFormat)
			: base(
					errorCode: errorCode ?? "MinLength",
					errorMessageFormat: errorMessageFormat ?? "Min length {0}")
		{
			Min = min;
		}

		public override string GetErrorMessage() => string.Format(ErrorMessageFormat, Min);

		public override string ToJson()
		{
			return "";
		}
	}
}
