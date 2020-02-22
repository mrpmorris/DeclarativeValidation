using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class ValidateHasMaxLengthExtension
	{
		public static IClassMemberValidator<TClass, TMember> HasMaxLength<TClass, TMember>(
			this IClassMemberValidator<TClass, TMember> memberValidator,
			int maximumLength,
			Func<TMember, string> getErrorMessage = null,
			string errorCode = nameof(HasMaxLength))
			where TMember : IEnumerable
		{
			if (maximumLength < 0)
				throw new ArgumentOutOfRangeException(nameof(maximumLength), "Cannot be less than 0");

			const string DefaultErrorMessageFormat = "Cannot be more than {0} in length.";

			var ruleEvaluator = new RuleEvaluator<TMember>(
				errorCode: errorCode,
				getErrorMessage: getErrorMessage ?? (value => string.Format(DefaultErrorMessageFormat, maximumLength)),
				isValidAsync: value => Task.FromResult(value.OfType<object>().Count() <= maximumLength));

			memberValidator.AddValidatorFactory(sp => ruleEvaluator);
			return memberValidator;
		}
	}
}
