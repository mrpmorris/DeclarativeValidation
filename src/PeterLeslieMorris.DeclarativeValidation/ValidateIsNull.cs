using System;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class ValidateIsNullExtension
	{
		public static IClassMemberValidator<TClass, TMember> IsNull<TClass, TMember>(
			this IClassMemberValidator<TClass, TMember> memberValidator,
			Func<TMember, string> getErrorMessage = null,
			string errorCode = nameof(IsNull))
			where TMember : class
		{
			var ruleEvaluator = new RuleEvaluator<TMember>(
				errorCode: errorCode,
				getErrorMessage: getErrorMessage ?? (value => "Cannot be set"),
				isValidAsync: value => Task.FromResult(value == null));

			memberValidator.AddRuleEvaluatorFactory(sp => ruleEvaluator);
			return memberValidator;
		}
	}
}
