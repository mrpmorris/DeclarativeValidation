using System.Collections;
using PeterLeslieMorris.DeclarativeValidation.Builders;
using PeterLeslieMorris.DeclarativeValidation.Factories;
using PeterLeslieMorris.DeclarativeValidation.Rules;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class MaxLengthExtensions
	{
		public static IMemberRuleBuilder<TClass, TProperty> MaxLength<TClass, TProperty>(
				this IMemberRuleBuilder<TClass, TProperty> builder,
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
