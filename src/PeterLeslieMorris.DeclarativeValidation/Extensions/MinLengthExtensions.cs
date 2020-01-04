using System.Collections;
using PeterLeslieMorris.DeclarativeValidation.Builders;
using PeterLeslieMorris.DeclarativeValidation.Factories;
using PeterLeslieMorris.DeclarativeValidation.Rules;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class MinLengthExtensions
	{
		public static MemberRuleBuilder<TClass, TProperty> MinLength<TClass, TProperty>(
				this MemberRuleBuilder<TClass, TProperty> builder,
				ulong min,
				string errorCode = null,
				string errorMessageFormat = null
			)
			where TClass : class
			where TProperty: IEnumerable
		{
			var factory = new RuleFactory<MinLengthRule>(x => {
				x.Min = min;
				x.ErrorCode = errorCode ?? x.ErrorCode;
				x.ErrorMessageFormat = errorMessageFormat ?? x.ErrorMessageFormat;
			});
			builder.AddRuleFactory(factory);
			return builder;
		}
	}
}
