using System;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public abstract class MemberRule : Rule
	{
		public string ErrorCode { get; set; }
		public string ErrorMessageFormat { get; set; }

		public abstract string GetErrorMessage();
	}
}
