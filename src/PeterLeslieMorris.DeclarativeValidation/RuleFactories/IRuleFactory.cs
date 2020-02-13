using System;

namespace PeterLeslieMorris.DeclarativeValidation.RuleFactories
{
	public interface IRuleFactory
	{
		IRule Create(IServiceProvider serviceProvider);
	}
}
