using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class ValidateIsInternetEmailAddressExtension
	{
		private static readonly Regex RegexValidator = new Regex(
			@"^(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)$",
			RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

		public static IClassMemberValidator<TClass, string> IsValidInternetEmailAddress<TClass>(
			this IClassMemberValidator<TClass, string> memberValidator,
			Func<string, string> getErrorMessage = null,
			string errorCode = nameof(IsValidInternetEmailAddress))
		{
			const string DefaultErrorMessageFormat = "Must be a valid Internet email address";

			var ruleEvaluator = new RuleEvaluator<string>(
				errorCode: errorCode,
				getErrorMessage: getErrorMessage ?? (value => string.Format(DefaultErrorMessageFormat, value)),
				isValidAsync: value => Task.FromResult(CheckValue(value)));

			memberValidator.AddRuleEvaluatorFactory(sp => ruleEvaluator);
			return memberValidator;
		}

		private static bool CheckValue(string value) =>
			value is null
			|| RegexValidator.IsMatch(value);
	}
}
