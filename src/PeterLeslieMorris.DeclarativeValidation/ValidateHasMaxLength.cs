using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class ValidateHasMaxLength<TValue> : IValueValidator<TValue>
		where TValue : IEnumerable
	{
		public int MaximumLength { get; }
		public string ErrorCode { get; }
		public string ErrorMessage => string.Format(ErrorMessageFormat, MaximumLength);
		public string ErrorMessageFormat { get; }

		public ValidateHasMaxLength(int maximumLength, string errorCode = null, string errorMessageFormat = null)
		{
			if (maximumLength < 0)
				throw new ArgumentOutOfRangeException("Cannot be less than 0", nameof(maximumLength));

			MaximumLength = maximumLength;
			ErrorCode = errorCode;
			ErrorMessageFormat = errorMessageFormat ?? "Cannot be more than {0} in length";
		}

		public Task<bool> IsValidAsync(TValue value) =>
			Task.FromResult(value.OfType<object>().Count() <= MaximumLength);
	}

	public static class ValidateHasMaxLengthExtension
	{
		public static IClassMemberValidator<TClass, TMember> HasMaxLength<TClass, TMember>(
			this IClassMemberValidator<TClass, TMember> memberValidator,
			int maximumLength,
			string errorMessageFormat = null,
			string errorCode = null)
			where TMember: IEnumerable
		{
			var validator = new ValidateHasMaxLength<TMember>(
				maximumLength: maximumLength,
				errorMessageFormat: errorMessageFormat,
				errorCode: errorCode);
			memberValidator.AddValidatorFactory(sp => validator);
			return memberValidator;
		}
	}
}
