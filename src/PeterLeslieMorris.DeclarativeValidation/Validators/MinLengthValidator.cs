using System.Collections;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class MinLengthExtension
	{
		public static IMemberRuleBuilder<TClass, TProperty> MinLength<TClass, TProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
				ulong min,
				string errorCode = null,
				string errorMessageFormat = null
			)
			where TClass : class
			where TProperty: IEnumerable
		{
			var rule = new MinLengthRule(
				min: min,
				errorCode: errorCode,
				errorMessageFormat: errorMessageFormat);
			builder.AddRule(rule);
			return builder;
		}
	}

	public class MinLengthRule : MemberRule
	{
		public readonly ulong Min;

		public MinLengthRule(ulong min, string errorCode, string errorMessageFormat)
			: base(
					errorCode: errorCode ?? "MinLength",
					errorMessageFormat: errorMessageFormat ?? "Min length {0}")
		{
			Min = min;
		}

		public override string GetErrorMessage() => string.Format(ErrorMessageFormat, Min);

		public override string ToJson()
		{
			return "";
		}
	}

}
