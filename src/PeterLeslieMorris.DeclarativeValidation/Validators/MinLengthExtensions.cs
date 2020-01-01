using System.Collections;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class MinLengthExtensions
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
}
