using PeterLeslieMorris.DeclarativeValidation.Factories;

namespace PeterLeslieMorris.DeclarativeValidation.Builders
{
	public abstract class RuleBuilder
	{
		internal abstract void AddRuleFactory(IRuleFactory ruleFactory);
	}
}
