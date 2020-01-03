using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation.Builders
{
	internal sealed class ClassRuleBuilder<TClass> : IClassRuleBuilder<TClass>
		where TClass : class
	{
		private readonly List<IRuleFactory> RuleFactories = new List<IRuleFactory>();

		internal ClassRuleBuilder() { }

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

		public void AddRuleFactory(IRuleFactory ruleFactory)
		{
			if (ruleFactory == null)
				throw new ArgumentNullException(nameof(ruleFactory));

			RuleFactories.Add(ruleFactory);
		}
	}
}
