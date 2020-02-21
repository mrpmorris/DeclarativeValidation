using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class ValidateIsNull<TMember> : IValueValidator<TMember>
		where TMember: class
	{
		public string ErrorCode { get; }
		public string ErrorMessage { get; }

		public ValidateIsNull(string errorCode = null, string errorMessage = null)
		{
			ErrorCode = errorCode;
			ErrorMessage = errorMessage ?? "Should be null";
		}

		public Task<bool> IsValidAsync(TMember value) =>
			Task.FromResult(value == null);
	}

	public static class IsNullValidatorExtension
	{
		public static IClassMemberValidator<TClass, TMember> IsNull<TClass, TMember>(
			this IClassMemberValidator<TClass, TMember> memberValidator,
			string errorMessage = null,
			string errorCode = null)
			where TMember: class
		{
			var validator = new ValidateIsNull<TMember>(
				errorCode: errorCode,
				errorMessage: errorMessage);
			memberValidator.AddValidatorFactory(sp => validator);
			return memberValidator;
		}
	}
}
