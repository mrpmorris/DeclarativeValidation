using System.Collections;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class MinimumLengthExtension
	{
		public static MemberRuleBuilder<TClass, TMember> MinimumLength<TClass, TMember>(
					this MemberRuleBuilder<TClass, TMember> builder,
					uint minimumLength)
					where TMember : IEnumerable
		{
			var factory = new MemberRuleFactory<MinimumLengthRule<TMember>>(r =>
			{
				r.MinimumLength = minimumLength;
			});
			return builder;
		}
	}
}
