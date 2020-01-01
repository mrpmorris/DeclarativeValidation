using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	internal sealed class ClassRuleBuilder<TClass> : IClassRuleBuilder<TClass>
		where TClass : class
	{
		internal ClassRuleBuilder() { }


		public IClassRuleBuilder<TClass> ForMember<TProperty>(
			Expression<Func<TClass, TProperty>> member,
			Action<IMemberRuleBuilder<TClass, TProperty>> validation)
		{
			var memberRuleBuilder = new MemberRuleBuilder<TClass, TProperty>(member);
			validation(memberRuleBuilder);
			return this;
		}

		public IClassRuleBuilder<TClass> ForMember<TProperty>(
			Expression<Func<TClass, TProperty?>> member,
			Action<IMemberRuleBuilder<TClass, TProperty?>> validation)
			where TProperty: struct
		{
			var memberRuleBuilder = new MemberRuleBuilder<TClass, TProperty?>(member);
			validation(memberRuleBuilder);
			return this;
		}
	}
}
