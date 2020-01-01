using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class LessThanExtension
	{
		public static IMemberRuleBuilder<TClass, TProperty> LessThan<TClass, TProperty, TOtherProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
				Expression<Func<TClass, TOtherProperty>> other,
				string errorMessage = null
			)
			where TClass : class
			where TProperty : IComparable<TOtherProperty>
		{
			return builder;
		}

		public static IMemberRuleBuilder<TClass, TProperty> LessThan<TClass, TProperty, TOtherProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
				Expression<Func<TClass, TOtherProperty?>> other,
				string errorMessage = null
			)
			where TClass : class
			where TProperty : IComparable<TOtherProperty>
			where TOtherProperty : struct
		{
			return builder;
		}


	}
}
