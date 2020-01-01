using System;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public abstract class MemberRule : Rule
	{
		public readonly string Member;
		public readonly string ErrorCode;
		public readonly string ErrorMessageFormat;

		public abstract string GetErrorMessage();

		public MemberRule(string member, string errorCode, string errorMessageFormat)
		{
			Member = member;
			ErrorCode = errorCode ?? throw new NullReferenceException(nameof(errorCode));
			ErrorMessageFormat = errorMessageFormat ?? throw new NullReferenceException(nameof(errorMessageFormat));
		}

	}
}
