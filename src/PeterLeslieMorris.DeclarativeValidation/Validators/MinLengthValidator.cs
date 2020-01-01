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
			return builder;
		}
	}
}
