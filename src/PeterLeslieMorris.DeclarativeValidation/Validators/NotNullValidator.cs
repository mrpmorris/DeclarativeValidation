namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class NotNullExtension
	{
		public static IMemberRuleBuilder<TClass, TProperty> NotNull<TClass, TProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
				string errorCode = null,
				string errorMessage = null
			)
			where TClass : class
			where TProperty : class
		{
			return builder;
		}

		public static IMemberRuleBuilder<TClass, TProperty?> NotNull<TClass, TProperty>(
				this IMemberRuleBuilder<TClass, TProperty?> builder,
				string errorCode = null,
				string errorMessage = null
			)
			where TClass : class
			where TProperty : struct
		{
			return builder;
		}
	}
}
