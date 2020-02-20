using System;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class ValidationError
	{
		public string MemberName { get; set; }
		public string MemberPath { get; set; }
		public string ErrorCode { get; set; }
		public string ErrorMessage { get; set; }

		private MemberIdentifier MemberIdentifier;
		private Func<MemberIdentifier> GetMemberIdentifierFunc;

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
			GetMemberIdentifierFunc = getMemberIdentifier;
		}

		public MemberIdentifier GetMemberIdentifier()
		{
			if (MemberIdentifier == null)
				MemberIdentifier = GetMemberIdentifierFunc();
			return MemberIdentifier;
		}

	}
}
