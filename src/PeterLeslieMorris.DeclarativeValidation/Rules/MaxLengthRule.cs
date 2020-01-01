namespace PeterLeslieMorris.DeclarativeValidation.Rules
{
	public class MaxLengthRule : MemberRule
	{
		public readonly ulong Max;

		public MaxLengthRule(ulong max, string errorCode, string errorMessageFormat)
			: base(
					errorCode: errorCode ?? "MaxLength",
					errorMessageFormat: errorMessageFormat ?? "Max length {0}")
		{
			Max = max;
		}

		public override string GetErrorMessage() => string.Format(ErrorMessageFormat, Max);

		public override string ToJson()
		{
			return "";
		}
	}
}
