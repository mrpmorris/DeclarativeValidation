using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class ValidateHasMinLengthExtension
	{
		public static IClassMemberValidator<TClass, TMember> HasMinLength<TClass, TMember>(
			this IClassMemberValidator<TClass, TMember> memberValidator,
			int minimumLength,
			Func<TMember, string> getErrorMessage = null,
			string errorCode = nameof(HasMinLength))
			where TMember : IEnumerable
		{
			if (minimumLength < 0)
				throw new ArgumentOutOfRangeException(nameof(minimumLength), "Cannot be less than 0");

			const string DefaultErrorMessageFormat = "Cannot be less than {0} in length.";

			var ruleEvaluator = new RuleEvaluator<TMember>(
				errorCode: errorCode,
				getErrorMessage: getErrorMessage ?? (value => string.Format(DefaultErrorMessageFormat, minimumLength)),
				isValidAsync: value => Task.FromResult(value.OfType<object>().Count() >= minimumLength));

			memberValidator.AddValidatorFactory(sp => ruleEvaluator);
			return memberValidator;
		}
	}
}
