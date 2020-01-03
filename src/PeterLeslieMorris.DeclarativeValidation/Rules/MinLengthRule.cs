﻿namespace PeterLeslieMorris.DeclarativeValidation.Rules
{
	public class MinLengthRule : MemberRule
	{
		public ulong Min { get; set; }

		public MinLengthRule()
		{
			ErrorCode = "MinLength";
			ErrorMessageFormat = "Minimum length {0}";
		}

		public override string GetErrorMessage() => string.Format(ErrorMessageFormat, Min);

		public override string ToJson()
		{
			return "";
		}
	}
}
