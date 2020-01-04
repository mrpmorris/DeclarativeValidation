﻿using System.Collections;
using PeterLeslieMorris.DeclarativeValidation.RuleBuilders;
using PeterLeslieMorris.DeclarativeValidation.RuleFactories;
using PeterLeslieMorris.DeclarativeValidation.Rules;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class MaxLengthExtensions
	{
		public static MemberRuleBuilder<TClass, TProperty> MaxLength<TClass, TProperty>(
				this MemberRuleBuilder<TClass, TProperty> builder,
				ulong max,
				string errorCode = null,
				string errorMessageFormat = null
			)
			where TClass : class
			where TProperty: IEnumerable
		{
			var factory = new RuleFactory<MaxLengthRule>(x => {
				x.Max = max;
				x.ErrorCode = errorCode ?? x.ErrorCode;
				x.ErrorMessageFormat = errorMessageFormat ?? x.ErrorMessageFormat;
			});
			builder.AddRuleFactory(factory);
			return builder;
		}
	}
}