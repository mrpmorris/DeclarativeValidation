using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class NotEqualToExtension
	{
		public static IMemberRuleBuilder<TClass, TProperty> NotEqualTo<TClass, TProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
				TProperty value,
				string errorCode = null,
				string errorMessageFormat = null
			)
			where TClass : class
			where TProperty : IEquatable<TProperty>
		{
			return builder;
		}

		public static IMemberRuleBuilder<TClass, TProperty> NotEqualTo<TClass, TProperty, TOtherProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
				Expression<Func<TClass, TOtherProperty>> other,
				string errorCode = null,
				string errorMessageFormat = null
			)
			where TClass : class
			where TProperty : IEquatable<TOtherProperty>
		{
			return builder;
		}

		public static IMemberRuleBuilder<TClass, TProperty> NotEqualTo<TClass, TProperty, TOtherProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
				Expression<Func<TClass, TOtherProperty?>> other,
				string errorCode = null,
				string errorMessageFormat = null
			)
			where TClass : class
			where TProperty : IEquatable<TOtherProperty>
			where TOtherProperty : struct
		{
			return builder;
		}


	}
}
