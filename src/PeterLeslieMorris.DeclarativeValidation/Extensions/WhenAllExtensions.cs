using System;
using System.Linq.Expressions;
using PeterLeslieMorris.DeclarativeValidation.RuleBuilders;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class WhenAllExtensions
	{
		public static ClassRuleBuilder<TClass> WhenAll<TClass, TProperty>(
				this ClassRuleBuilder<TClass> builder,
				Expression<Func<TClass, TProperty>> member,
				Action<MemberRuleBuilder<TClass, TProperty>> condition,
				Action<ClassRuleBuilder<TClass>> ruleBuilder
			)
			where TClass : class
		{
			return builder;
		}
	}
}
