using System;
using System.Linq.Expressions;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public abstract class ClassValidator<TClass>
	{
		public void For<TMember>(
			Expression<Func<TClass, TMember>> member,
			Action<MemberRuleBuilder<TClass, TMember>> validation)
		{
			var memberValidator = new MemberRuleBuilder<TClass, TMember>(this);
			validation(memberValidator);
		}
	}
}
