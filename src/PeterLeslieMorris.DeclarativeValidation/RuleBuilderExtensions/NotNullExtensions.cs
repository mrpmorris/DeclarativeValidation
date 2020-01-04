using PeterLeslieMorris.DeclarativeValidation.RuleBuilders;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class NotNullExtensions
	{
		public static MemberRuleBuilder<TClass, TProperty> NotNull<TClass, TProperty>(
				this MemberRuleBuilder<TClass, TProperty> builder,
				string errorCode = null,
				string errorMessage = null
			)
			where TClass : class
			where TProperty : class
		{
			return builder;
		}

		public static MemberRuleBuilder<TClass, TProperty?> NotNull<TClass, TProperty>(
				this MemberRuleBuilder<TClass, TProperty?> builder,
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
