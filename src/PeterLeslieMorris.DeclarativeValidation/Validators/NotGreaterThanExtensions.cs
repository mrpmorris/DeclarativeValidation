using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class NotGreaterThanExtensions
	{
		public static IMemberRuleBuilder<TClass, TProperty> NotGreaterThan<TClass, TProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
				TProperty value,
				string errorCode = null,
				string errorMessageFormat = null
			)
			where TClass : class
			where TProperty : IComparable<TProperty>
		{
			return builder;
		}

		public static IMemberRuleBuilder<TClass, TProperty> NotGreaterThan<TClass, TProperty, TOtherProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
				Expression<Func<TClass, TOtherProperty>> other,
				string errorCode = null,
				string errorMessageFormat = null
			)
			where TClass : class
			where TProperty : IComparable<TOtherProperty>
		{
			return builder;
		}

		public static IMemberRuleBuilder<TClass, TProperty> NotGreaterThan<TClass, TProperty, TOtherProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
				Expression<Func<TClass, TOtherProperty?>> other,
				string errorCode = null,
				string errorMessageFormat = null
			)
			where TClass : class
			where TProperty : IComparable<TOtherProperty>
			where TOtherProperty : struct
		{
			return builder;
		}
	}
}
