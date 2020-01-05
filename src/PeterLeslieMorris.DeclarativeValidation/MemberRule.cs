namespace PeterLeslieMorris.DeclarativeValidation
{
	public abstract class MemberRule : Rule
	{
		public abstract string GetErrorMessage();

		public string MemberPath { get; set; }
		public string ErrorCode { get; set; }
		public string ErrorMessageFormat { get; set; }

		public virtual RuleViolation ToRuleViolation()
			=> new RuleViolation(
				memberPath: MemberPath,
				errorCode: ErrorCode,
				errorMessage: GetErrorMessage());
	}
}
