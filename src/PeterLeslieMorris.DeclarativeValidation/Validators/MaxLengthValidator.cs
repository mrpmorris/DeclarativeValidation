using System.Collections;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class MaxLengthExtension
	{
		public static IMemberRuleBuilder<TClass, TProperty> MaxLength<TClass, TProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
				ulong max,
				string errorCode = null,
				string errorMessage = null
			)
			where TClass : class
			where TProperty: IEnumerable
		{
			return builder;
		}
	}
}
