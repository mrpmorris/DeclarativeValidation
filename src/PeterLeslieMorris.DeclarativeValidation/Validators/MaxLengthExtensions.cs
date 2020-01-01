using System.Collections;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class MaxLengthExtensions
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
			var rule = new MaxLengthRule(
				max: max,
				errorCode: errorCode,
				errorMessageFormat: errorMessageFormat);
			builder.AddRule(rule);
			return builder;
		}
	}
}
