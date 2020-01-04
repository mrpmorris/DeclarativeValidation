﻿using System;
using System.Linq.Expressions;
using PeterLeslieMorris.DeclarativeValidation.Builders;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class WhenAnyExtensions
	{
		public static ClassRuleBuilder<TClass> WhenAny<TClass, TProperty>(
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
