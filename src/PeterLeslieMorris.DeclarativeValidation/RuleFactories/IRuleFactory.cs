using System;

namespace PeterLeslieMorris.DeclarativeValidation.RuleFactories
{
	public interface IRuleFactory
	{
		Rule Create(IServiceProvider serviceProvider);
	}
}
