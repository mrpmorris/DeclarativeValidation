using System.Collections;
using PeterLeslieMorris.DeclarativeValidation.RuleBuilders;
using PeterLeslieMorris.DeclarativeValidation.RuleFactories;
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
			var factory = new MemberRuleFactory<MinLengthRule>(x => {
				x.Min = min;
			});
			builder.InternalAddRuleFactory(factory);
			return builder;
		}
	}
}
