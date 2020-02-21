using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation.Extensions
{
	internal static class ExpressionExtensions
	{
		public static (string memberName, string memberPath) GetMemberNameAndPath<TClass, TMember>(
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
			string memberPath = string.Join('.', parts);
			int lastDotIndex = memberPath.LastIndexOf('.');
			string memberName = lastDotIndex < 0
				? memberPath
				: memberPath.Remove(0, lastDotIndex + 1);

			return (memberName, memberPath);
		}

		public static void ParseAccessor<TClass, TMember>(
			this Expression<Func<TClass, TMember>> accessor,
			out Func<TClass, object> getOwner,
			out string memberName)
		{
			var accessorBody = accessor.Body;

			// Unwrap casts to object
			if (accessorBody is UnaryExpression unaryExpression
					&& unaryExpression.NodeType == ExpressionType.Convert
					&& unaryExpression.Type == typeof(object))
			{
				accessorBody = unaryExpression.Operand;
			}

			if (!(accessorBody is MemberExpression memberExpression))
				throw new ArgumentException($"The provided expression contains a {accessorBody.GetType().Name} which is not supported. Only simple member accessors (fields, properties) of an object are supported.");

			// Identify the field name. We don't mind whether it's a property or field, or even something else.
			memberName = memberExpression.Member.Name;

			// Get a reference to the model object
			// i.e., given an value like "(something).MemberName", determine the runtime value of "(something)",
			switch (memberExpression.Expression)
			{
				case ConstantExpression constantExpression:
					getOwner = x => constantExpression.Value;
					break;
				default:
					// It would be great to cache this somehow, but it's unclear there's a reasonable way to do
					// so, given that it embeds captured values such as "this". We could consider special-casing
					// for "() => something.Member" and building a cache keyed by "something.GetType()" with values
					// of type Func<object, object> so we can cheaply map from "something" to "something.Member".
					var modelLambda = Expression.Lambda(memberExpression.Expression, accessor.Parameters);
					var modelLambdaCompiled = (Func<TClass, object>)modelLambda.Compile();
					getOwner = modelLambdaCompiled;
					break;
			}
		}
	}
}
