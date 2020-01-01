using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class WhenAllExtension
	{
		public static IClassRuleBuilder<TClass> WhenAll<TClass, TProperty>(
				this IClassRuleBuilder<TClass> builder,
				Expression<Func<TClass, TProperty>> member,
				Action<IMemberRuleBuilder<TClass, TProperty>> condition,
				Action<IClassRuleBuilder<TClass>> ruleBuilder
			)
			where TClass : class
		{
			return builder;
		}
	}
}
