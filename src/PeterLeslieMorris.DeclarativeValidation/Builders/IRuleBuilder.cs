using PeterLeslieMorris.DeclarativeValidation.Factories;

namespace PeterLeslieMorris.DeclarativeValidation.Builders
{
	public interface IRuleBuilder
	{
		void AddRuleFactory(IRuleFactory ruleFactory);
	}
}
