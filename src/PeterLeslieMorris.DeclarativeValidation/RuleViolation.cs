using System;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class RuleViolation
	{
		public string MemberPath { get; set; }
		public string ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
		public readonly Func<MemberIdentifier> GetMemberIdentifier;

		public RuleViolation() { }

		public RuleViolation(
			string memberPath,
			string errorCode,
			string errorMessage,
			Func<MemberIdentifier> getMemberIdentifier)
		{
			MemberPath = memberPath;
			ErrorCode = errorCode;
			ErrorMessage = errorMessage;
			GetMemberIdentifier = getMemberIdentifier;
		}
	}
}
