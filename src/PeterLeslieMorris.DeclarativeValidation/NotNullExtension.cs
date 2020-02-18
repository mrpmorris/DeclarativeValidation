using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class NotNullExtension
	{
		public static MemberRuleBuilder<TClass, TMember> NotNull<TClass, TMember>(
			this MemberRuleBuilder<TClass, TMember> builder)
			where TMember: class
		{
			var factory = new MemberRuleFactory<NotNullRule<TMember>>(r =>
			{
			});
			builder.AddRuleFactory(factory);
			return builder;
		}
	}
}
