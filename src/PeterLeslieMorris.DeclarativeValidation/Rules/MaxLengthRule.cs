namespace PeterLeslieMorris.DeclarativeValidation.Rules
{
	public class MaxLengthRule : MemberRule
	{
		public ulong Max { get; set; }

		public MaxLengthRule()
		{
			ErrorCode = "MaxLength";
			ErrorMessageFormat = "Maximum length {0}";
		}

		public override string GetErrorMessage() => string.Format(ErrorMessageFormat, Max);

		public override string ToJson()
		{
			return "";
		}
	}
}
