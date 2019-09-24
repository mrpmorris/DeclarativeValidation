using System;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public interface IAggregateRootValidator { }
	public abstract class AggregateRootValidator<TClass> : ClassValidator<TClass>, IAggregateRootValidator
	{
		public ClassValidator<TClass> Ensure => this;
	}
}
