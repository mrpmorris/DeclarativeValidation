using System.Collections;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class MaxLengthExtension
	{
		public static IMemberRuleBuilder<TClass, TProperty> MaxLength<TClass, TProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
				ulong max,
				string errorCode = null,
				string errorMessageFormat = null
			)
			where TClass : class
			where TProperty: IEnumerable
		{
			var validator = new MaxLengthRule(
				max: max,
				errorCode: errorCode,
				errorMessageFormat: errorMessageFormat);
			return builder;
		}
	}

	public class MaxLengthRule : Rule
	{
		public readonly ulong Max;

		public MaxLengthRule(ulong max, string errorCode, string errorMessageFormat)
			: base(
					errorCode: errorCode ?? "MaxLength",
					errorMessageFormat: errorMessageFormat ?? "Max length {0}")
		{
			Max = max;
		}

		public override string ToJson()
		{
			return "";
		}
	}
}
