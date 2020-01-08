using System;

namespace PeterLeslieMorris.DeclarativeValidation.RuleFactories
{
	public interface IRuleFactory
	{
		string MemberPath { get; set;  }
		IRule Create(IServiceProvider serviceProvider);
	}
}
