namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class NotNullExtension
	{
		public static IMemberRuleBuilder<TClass, TProperty> NotNull<TClass, TProperty>(
			this IMemberRuleBuilder<TClass, TProperty> builder,
			string errorCode = "NotNull")
			where TClass : class
		{
			return builder;
		}
	}
}
