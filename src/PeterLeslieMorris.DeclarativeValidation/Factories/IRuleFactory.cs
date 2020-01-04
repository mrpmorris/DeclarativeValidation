using System;

namespace PeterLeslieMorris.DeclarativeValidation.Factories
{
	public interface IRuleFactory
	{
		Rule Create(IServiceProvider serviceProvider);
	}
}
