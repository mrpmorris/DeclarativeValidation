using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class EqualToExtension
	{
		public static IMemberRuleBuilder<TClass, TProperty> EqualTo<TClass, TProperty, TOtherProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
				Expression<Func<TClass, TOtherProperty>> other,
				string errorCode = null
			)
			where TClass : class
			where TProperty : IEquatable<TOtherProperty>
		{
			return builder;
		}

		public static IMemberRuleBuilder<TClass, TProperty> EqualTo<TClass, TProperty, TOtherProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
				Expression<Func<TClass, TOtherProperty?>> other,
				string errorCode = null
			)
			where TClass : class
			where TProperty : IEquatable<TOtherProperty>
			where TOtherProperty : struct
		{
			return builder;
		}


	}
}
