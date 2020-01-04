using PeterLeslieMorris.DeclarativeValidation.RuleFactories;

namespace PeterLeslieMorris.DeclarativeValidation.RuleBuilders
{
	public abstract class RuleBuilder
	{
		internal abstract void InternalAddRuleFactory(IRuleFactory ruleFactory);
	}
}
