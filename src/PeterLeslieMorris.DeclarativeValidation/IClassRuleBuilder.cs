using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IClassRuleBuilder : IRuleBuilder
	{
	}

	public interface IClassRuleBuilder<TClass> : IClassRuleBuilder
		where TClass: class
	{
		IClassRuleBuilder<TClass> ForMember<TProperty>(
			Expression<Func<TClass, TProperty>> member,
			Action<IMemberRuleBuilder<TClass, TProperty>> validation);
	}
}
