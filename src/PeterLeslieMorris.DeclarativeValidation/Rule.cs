using System;
using System.Linq.Expressions;
using PeterLeslieMorris.DeclarativeValidation.Extensions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public abstract class Rule<TSource, TMember>
	{
		public readonly string MemberName;
		public readonly Expression<Func<TSource, TMember>> Expression;

		protected readonly Func<TSource, object> GetOwningObjectFunc;
		protected readonly Func<TSource, TMember> GetMemberValueFunc;

		protected Rule(Expression<Func<TSource, TMember>> expression)
		{
			Expression = expression ?? throw new ArgumentNullException(nameof(expression));
			expression.ParseAccessor(out MemberName, out GetOwningObjectFunc);
		}
	}
}
