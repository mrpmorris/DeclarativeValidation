using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class ExpressionExtensions
	{
		public static string GetPath<TClass, TProperty>(this Expression<Func<TClass, TProperty>> member)
		{
			var memberExpression = (MemberExpression)member.Body;

			var names = new Stack<string>();
			while (memberExpression != null)
			{
				names.Push(memberExpression.Member.Name);
				memberExpression = memberExpression.Expression as MemberExpression;
			}

			return string.Join('.', names);
		}
	}
}
