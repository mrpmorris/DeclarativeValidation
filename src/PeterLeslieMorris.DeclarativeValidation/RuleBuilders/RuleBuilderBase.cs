using PeterLeslieMorris.DeclarativeValidation.RuleFactories;

namespace PeterLeslieMorris.DeclarativeValidation.RuleBuilders
{
	public abstract class RuleBuilderBase
	{
		internal abstract void InternalAddRuleFactory(IRuleFactory ruleFactory);
	}
}
