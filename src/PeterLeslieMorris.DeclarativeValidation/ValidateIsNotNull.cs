using System;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class ValidateIsNotNullExtension
	{
		public static IClassMemberValidator<TClass, TMember> IsNotNull<TClass, TMember>(
			this IClassMemberValidator<TClass, TMember> memberValidator,
			Func<TMember, string> getErrorMessage = null,
			string errorCode = nameof(IsNotNull))
			where TMember : class
		{
			var ruleEvaluator = new RuleEvaluator<TMember>(
				errorCode: errorCode,
				getErrorMessage: getErrorMessage ?? (value => "Required"),
				isValidAsync: value => Task.FromResult(value != null));

			memberValidator.AddRuleEvaluatorFactory(sp => ruleEvaluator);
			return memberValidator;
		}
	}
}
