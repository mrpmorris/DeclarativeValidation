using System;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public abstract class MemberRule : Rule
	{
		public readonly string ErrorCode;
		public readonly string ErrorMessageFormat;

		public abstract string GetErrorMessage();

		public MemberRule(string errorCode, string errorMessageFormat)
		{
			ErrorCode = errorCode ?? throw new NullReferenceException(nameof(errorCode));
			ErrorMessageFormat = errorMessageFormat ?? throw new NullReferenceException(nameof(errorMessageFormat));
		}

	}
}
