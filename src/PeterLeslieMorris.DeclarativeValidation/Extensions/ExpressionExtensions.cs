using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Extensions
{
	internal static class ExpressionExtensions
	{
		public static string GetMemberPath<TClass, TMember>(
			this Expression<Func<TClass, TMember>> member)
		{
			// https://stackoverflow.com/questions/1667408/c-getting-names-of-properties-in-a-chain-from-lambda-expression

			MemberExpression me;
			switch (member.Body.NodeType)
			{
				case ExpressionType.Convert:
				case ExpressionType.ConvertChecked:
					var ue = member.Body as UnaryExpression;
					me = ((ue != null) ? ue.Operand : null) as MemberExpression;
					break;
				default:
					me = member.Body as MemberExpression;
					break;
			}

			var parts = new Stack<string>();
			while (me != null)
			{
				string propertyName = me.Member.Name;
				parts.Push(propertyName);
				me = me.Expression as MemberExpression;
			}
			return string.Join('.', parts);
		}
	}
}
