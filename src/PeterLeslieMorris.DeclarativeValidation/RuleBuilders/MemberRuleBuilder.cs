using System;
using System.Linq.Expressions;
using PeterLeslieMorris.DeclarativeValidation.RuleFactories;

namespace PeterLeslieMorris.DeclarativeValidation.RuleBuilders
{
	public abstract class MemberRuleBuilder : RuleBuilder
	{
		internal string Member { get; set; }
	}

	public class MemberRuleBuilder<TClass, TProperty> : MemberRuleBuilder
		where TClass : class
	{
		private readonly RuleBuilder Parent;

		internal MemberRuleBuilder(RuleBuilder parent, Expression<Func<TClass, TProperty>> member)
		{
			Parent = parent;
			Member = member.GetPath();
		}

		internal override void AddRuleFactory(IRuleFactory ruleFactory)
		{
			Parent.AddRuleFactory(ruleFactory);
		}
	}
}
