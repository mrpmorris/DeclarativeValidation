using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PeterLeslieMorris.DeclarativeValidation.Factories;

namespace PeterLeslieMorris.DeclarativeValidation.Builders
{
	internal sealed class ClassRuleBuilder<TClass> : IClassRuleBuilder<TClass>
		where TClass : class
	{
		private readonly List<IRuleFactory> RuleFactories = new List<IRuleFactory>();

		internal ClassRuleBuilder() { }

		public Type ClassType { get => typeof(TClass); }

		public void AddRuleFactory(IRuleFactory ruleFactory)
		{
			RuleFactories.Add(ruleFactory);
		}

		public ClassRuleFactory CreateRuleFactory() => new ClassRuleFactory(RuleFactories);

		public IClassRuleBuilder<TClass> ForMember<TProperty>(
			Expression<Func<TClass, TProperty>> member,
			Action<IMemberRuleBuilder<TClass, TProperty>> validation)
		{
			var memberRuleBuilder = new MemberRuleBuilder<TClass, TProperty>(this, member);
			validation(memberRuleBuilder);
			return this;
		}

		public IClassRuleBuilder<TClass> ForMember<TProperty>(
			Expression<Func<TClass, TProperty?>> member,
			Action<IMemberRuleBuilder<TClass, TProperty?>> validation)
			where TProperty: struct
		{
			var memberRuleBuilder = new MemberRuleBuilder<TClass, TProperty?>(this, member);
			validation(memberRuleBuilder);
			return this;
		}
	}
}
