using PeterLeslieMorris.DeclarativeValidation.RuleFactories;

namespace PeterLeslieMorris.DeclarativeValidation.RuleBuilders
{
	public static class RuleBuilderExtensions
	{
		public static void AddRuleFactory(this RuleBuilder builder, IRuleFactory ruleFactory)
		{
			builder.InternalAddRuleFactory(ruleFactory);
		}
	}
}
