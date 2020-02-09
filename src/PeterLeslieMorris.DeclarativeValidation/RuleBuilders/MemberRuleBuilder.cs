using System;
using System.Linq.Expressions;
using PeterLeslieMorris.DeclarativeValidation.RuleFactories;

namespace PeterLeslieMorris.DeclarativeValidation.RuleBuilders
{
	public abstract class MemberRuleBuilder : RuleBuilder
	{
		internal string MemberPath { get; set; }
	}

	public class MemberRuleBuilder<TClass, TProperty> : MemberRuleBuilder
		where TClass : class
	{
		private readonly RuleBuilder Parent;
		public readonly Func<TClass, TProperty> GetMemberValueFunc;
		public readonly Func<object> GetPropertyOwnerFunc;

		internal MemberRuleBuilder(RuleBuilder parent, Expression<Func<TClass, TProperty>> member)
		{
			Parent = parent;
			MemberPath = member.GetPath();
			GetMemberValueFunc = member.Compile();
			GetPropertyOwnerFunc = () => null;
		}

		internal override void InternalAddRuleFactory(IRuleFactory ruleFactory)
		{
			if (ruleFactory.MemberPath == null)
				ruleFactory.MemberPath = MemberPath;
			Parent.InternalAddRuleFactory(ruleFactory);
		}
	}
}
