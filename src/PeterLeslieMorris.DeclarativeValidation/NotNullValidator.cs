using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class NotNullValidator<TMember> : IValueValidator<TMember>
		where TMember: class
	{
		public string ErrorCode { get; }
		public string ErrorMessage { get; }

		public NotNullValidator(string errorCode = null, string errorMessage = null)
		{
			ErrorCode = errorCode;
			ErrorMessage = errorMessage ?? "Required";
		}

		public Task<bool> IsValidAsync(TMember value) =>
			Task.FromResult(value != null);
	}

	public static class NotNullValidatorExtension
	{
		public static IClassMemberValidator<TClass, TMember> NotNull<TClass, TMember>(
			this IClassMemberValidator<TClass, TMember> memberValidator,
			string errorMessage = null,
			string errorCode = null)
			where TMember: class
		{
			var validator = new NotNullValidator<TMember>(
				errorCode: errorCode,
				errorMessage: errorMessage);
			memberValidator.AddValidatorFactory(sp => validator);
			return memberValidator;
		}
	}
}
