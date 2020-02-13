using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PeterLeslieMorris.DeclarativeValidation.RuleFactories;

namespace PeterLeslieMorris.DeclarativeValidation.RuleBuilders
{
	public abstract class ClassRuleBuilder : RuleBuilderBase
	{
		internal abstract Type ClassType { get; }
		internal abstract ClassRuleFactory CreateRuleFactory();
	}

	public sealed class ClassRuleBuilder<TClass> : ClassRuleBuilder
		where TClass : class
	{
		private readonly List<IRuleFactory> RuleFactories = new List<IRuleFactory>();

		internal ClassRuleBuilder() { }

		public ClassRuleBuilder<TClass> ForMember<TProperty>(
			Expression<Func<TClass, TProperty>> member,
			Action<MemberRuleBuilder<TClass, TProperty>> validation)
		{
			var memberRuleBuilder = new MemberRuleBuilder<TClass, TProperty>(this, member);
			validation(memberRuleBuilder);
			return this;
		}

		public ClassRuleBuilder<TClass> ForMember<TProperty>(
			Expression<Func<TClass, TProperty?>> member,
			Action<MemberRuleBuilder<TClass, TProperty?>> validation)
			where TProperty : struct
		{
			var memberRuleBuilder = new MemberRuleBuilder<TClass, TProperty?>(this, member);
			validation(memberRuleBuilder);
			return this;
		}

		// Internal
		internal override Type ClassType { get => typeof(TClass); }

		internal override void InternalAddRuleFactory(IRuleFactory ruleFactory)
		{
			RuleFactories.Add(ruleFactory);
		}

		internal override ClassRuleFactory CreateRuleFactory() => new ClassRuleFactory(RuleFactories);
	}
}
