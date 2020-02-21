using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class ValidateHasMinLength<TValue> : IValueValidator<TValue>
		where TValue : IEnumerable
	{
		public int MinimumLength { get; }
		public string ErrorCode { get; }
		public string ErrorMessage => string.Format(ErrorMessageFormat, MinimumLength);
		public string ErrorMessageFormat { get; }

		public ValidateHasMinLength(int minimumLength, string errorCode = null, string errorMessageFormat = null)
		{
			if (minimumLength < 0)
				throw new ArgumentOutOfRangeException("Cannot be less than 0", nameof(minimumLength));

			MinimumLength = minimumLength;
			ErrorCode = errorCode;
			ErrorMessageFormat = errorMessageFormat ?? "Must be at least {0} in length";
		}

		public Task<bool> IsValidAsync(TValue value) =>
			Task.FromResult(value.OfType<object>().Count() >= MinimumLength);
	}

	public static class ValidateHasMinLengthExtension
	{
		public static IClassMemberValidator<TClass, TMember> HasMinLength<TClass, TMember>(
			this IClassMemberValidator<TClass, TMember> memberValidator,
			int minimumLength,
			string errorMessageFormat = null,
			string errorCode = null)
			where TMember: IEnumerable
		{
			var validator = new ValidateHasMinLength<TMember>(
				minimumLength: minimumLength,
				errorMessageFormat: errorMessageFormat,
				errorCode: errorCode);
			memberValidator.AddValidatorFactory(sp => validator);
			return memberValidator;
		}
	}
}
