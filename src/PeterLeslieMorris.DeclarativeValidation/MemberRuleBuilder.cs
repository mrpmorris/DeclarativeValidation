using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	internal class MemberRuleBuilder<TClass, TProperty> : IMemberRuleBuilder<TClass, TProperty>
		where TClass : class
	{
		public readonly string Member;

		internal MemberRuleBuilder(Expression<Func<TClass, TProperty>> member)
		{
			Member = member.GetPath();
		}
	}
}
