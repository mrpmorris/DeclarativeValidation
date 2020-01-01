namespace PeterLeslieMorris.DeclarativeValidation
{
	public abstract class MemberRule : Rule
	{
		public string Member { get; private set; }
		public string ErrorCode { get; private set; }

		protected MemberRule(string member, string errorCode)
		{
			Member = member;
			ErrorCode = errorCode;
		}
	}
}
