using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation.Builders
{
	internal class MemberRuleBuilder<TClass, TProperty> : IMemberRuleBuilder<TClass, TProperty>
		where TClass : class
	{
		private readonly IRuleBuilder Parent;

		public string Member { get; }

		internal MemberRuleBuilder(IRuleBuilder parent, Expression<Func<TClass, TProperty>> member)
		{
			Parent = parent;
			Member = member.GetPath();
		}

		public void AddRuleFactory(IRuleFactory ruleFactory)
		{
			Parent.AddRuleFactory(ruleFactory);
		}
	}
}
