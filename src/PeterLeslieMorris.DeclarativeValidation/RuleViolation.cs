using System;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class ValidationError
	{
		public string MemberName { get; set; }
		public string MemberPath { get; set; }
		public string ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
		public readonly Func<MemberIdentifier> GetMemberIdentifier;

		public ValidationError() { }

		public ValidationError(
			string memberName,
			string memberPath,
			string errorCode,
			string errorMessage,
			Func<MemberIdentifier> getMemberIdentifier)
		{
			MemberName = memberName;
			MemberPath = memberPath;
			ErrorCode = errorCode;
			ErrorMessage = errorMessage;
			GetMemberIdentifier = getMemberIdentifier;
		}
	}
}
