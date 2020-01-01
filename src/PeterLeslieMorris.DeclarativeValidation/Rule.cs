using System;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public abstract class Rule
	{
		public readonly string ErrorCode;
		public readonly string ErrorMessageFormat;

		public abstract string ToJson();
		public virtual string GetErrorMessage() => ErrorMessageFormat;

		protected Rule(string errorCode, string errorMessageFormat)
		{
			ErrorCode = errorCode ?? throw new NullReferenceException(nameof(errorCode));
			ErrorMessageFormat = errorMessageFormat ?? throw new NullReferenceException(nameof(errorMessageFormat));
		}

	}
}
