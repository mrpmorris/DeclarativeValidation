using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class NotLessThanExtension
	{
		public static IMemberRuleBuilder<TClass, TProperty> NotLessThan<TClass, TProperty, TOtherProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
				Expression<Func<TClass, TOtherProperty>> other
			)
			where TClass : class
			where TProperty : IComparable<TOtherProperty>
		{
			return builder;
		}
	}
}
