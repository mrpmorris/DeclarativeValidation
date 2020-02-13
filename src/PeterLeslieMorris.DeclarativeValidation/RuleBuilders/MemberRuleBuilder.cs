using System;
using System.Linq.Expressions;
using PeterLeslieMorris.DeclarativeValidation.RuleFactories;

namespace PeterLeslieMorris.DeclarativeValidation.RuleBuilders
{
	public abstract class MemberRuleBuilder : RuleBuilderBase
	{
		internal string MemberPath { get; set; }
	}

	public class MemberRuleBuilder<TClass, TProperty> : MemberRuleBuilder
		where TClass : class
	{
		private readonly RuleBuilderBase Parent;
		public readonly Func<TClass, TProperty> GetMemberValueFunc;
		public readonly Func<object> GetPropertyOwnerFunc;

		internal MemberRuleBuilder(RuleBuilderBase parent, Expression<Func<TClass, TProperty>> member)
		{
			Parent = parent;
			MemberPath = member.GetPath();
			GetMemberValueFunc = member.Compile();
			GetPropertyOwnerFunc = () => null;
		}

		internal override void InternalAddRuleFactory(IRuleFactory ruleFactory)
		{
			Parent.InternalAddRuleFactory(ruleFactory);
		}
	}
}
