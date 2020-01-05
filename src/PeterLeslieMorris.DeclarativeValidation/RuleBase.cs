using System;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public abstract class RuleBase : IRule
	{
		public string MemberPath { get; set; }
		public string ErrorCode { get; set; }
		public string ErrorMessageFormat { get; set; }

		public virtual string GetErrorMessage() => ErrorMessageFormat;
		public abstract string ToJson();
		public abstract Task ValidateAsync(ValidationContext context);

		protected RuleBase(string errorCode, string errorMessageFormat)
		{
			ErrorCode = errorCode;
			ErrorMessageFormat = errorMessageFormat ?? throw new ArgumentNullException(nameof(errorCode));
		}

		public virtual RuleViolation CreateRuleViolation()
			=> new RuleViolation(
				memberPath: MemberPath,
				errorCode: ErrorCode,
				errorMessage: GetErrorMessage());
	}
}
