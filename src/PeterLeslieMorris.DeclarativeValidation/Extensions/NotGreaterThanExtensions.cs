using System;
using System.Linq.Expressions;
using PeterLeslieMorris.DeclarativeValidation.RuleBuilders;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class NotGreaterThanExtensions
	{
		public static MemberRuleBuilder<TClass, TProperty> NotGreaterThan<TClass, TProperty>(
				this MemberRuleBuilder<TClass, TProperty> builder,
				TProperty value,
				string errorCode = null,
				string errorMessageFormat = null
			)
			where TClass : class
			where TProperty : IComparable<TProperty>
		{
			return builder;
		}

		public static MemberRuleBuilder<TClass, TProperty> NotGreaterThan<TClass, TProperty, TOtherProperty>(
				this MemberRuleBuilder<TClass, TProperty> builder,
				Expression<Func<TClass, TOtherProperty>> other,
				string errorCode = null,
				string errorMessageFormat = null
			)
			where TClass : class
			where TProperty : IComparable<TOtherProperty>
		{
			return builder;
		}

		public static MemberRuleBuilder<TClass, TProperty> NotGreaterThan<TClass, TProperty, TOtherProperty>(
				this MemberRuleBuilder<TClass, TProperty> builder,
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
