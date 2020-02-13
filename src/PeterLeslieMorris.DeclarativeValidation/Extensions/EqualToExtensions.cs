﻿using System;
using System.Linq.Expressions;
using PeterLeslieMorris.DeclarativeValidation.RuleBuilders;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class EqualToExtensions
	{
		public static MemberRuleBuilder<TClass, TProperty> EqualTo<TClass, TProperty>(
				this MemberRuleBuilder<TClass, TProperty> builder,
				TProperty value,
				string errorCode = null,
				string errorMessageFormat = null
			)
			where TClass : class
			where TProperty : IEquatable<TProperty>
		{
			return builder;
		}

		public static MemberRuleBuilder<TClass, TProperty> EqualTo<TClass, TProperty, TOtherProperty>(
				this MemberRuleBuilder<TClass, TProperty> builder,
				Expression<Func<TClass, TOtherProperty>> other,
				string errorCode = null,
				string errorMessageFormat = null
			)
			where TClass : class
			where TProperty : IEquatable<TOtherProperty>
		{
			return builder;
		}

		public static MemberRuleBuilder<TClass, TProperty> EqualTo<TClass, TProperty, TOtherProperty>(
				this MemberRuleBuilder<TClass, TProperty> builder,
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