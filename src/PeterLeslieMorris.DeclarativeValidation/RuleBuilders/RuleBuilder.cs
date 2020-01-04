using PeterLeslieMorris.DeclarativeValidation.Factories;

namespace PeterLeslieMorris.DeclarativeValidation.RuleBuilders
{
	public abstract class RuleBuilder
	{
		internal abstract void AddRuleFactory(IRuleFactory ruleFactory);
	}
}
