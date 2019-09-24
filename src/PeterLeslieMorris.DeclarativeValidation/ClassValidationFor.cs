using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public abstract class ClassValidationFor<TSource>
	{
		protected void MemberRuleFor<TMember>(Expression<Func<TSource, TMember>> memberExpression)
		{

		}
	}
}
