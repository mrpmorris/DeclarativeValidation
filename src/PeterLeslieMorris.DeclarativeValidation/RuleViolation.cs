namespace PeterLeslieMorris.DeclarativeValidation
{
	public class RuleViolation
	{
		public string MemberPath { get; set; }
		public string ErrorCode { get; set; }
		public string ErrorMessage { get; set; }

		public RuleViolation() { }

		public RuleViolation(string memberPath, string errorCode, string errorMessage)
		{
			MemberPath = memberPath;
			ErrorCode = errorCode;
			ErrorMessage = errorMessage;
		}
	}
}
