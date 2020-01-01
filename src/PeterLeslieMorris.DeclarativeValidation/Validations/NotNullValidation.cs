namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class NotNullExtension
	{
		public static IMemberRuleBuilder<TClass, TProperty> NotNull<TClass, TProperty>(
			this IMemberRuleBuilder<TClass, TProperty> memberValidationBuilder,
			string errorCode = "NotNull")
			where TClass : class
		{
			return memberValidationBuilder;
		}
	}
}
