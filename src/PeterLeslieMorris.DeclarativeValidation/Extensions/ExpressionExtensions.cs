using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation.Extensions
{
	internal static class ExpressionExtensions
	{
		public static void ParseAccessor<TSource, TMember>(
			this Expression<Func<TSource, TMember>> accessor,
			out string memberName,
			out Func<TSource, object> getOwningObjectFunc)
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
			{
				throw new ArgumentException($"The provided expression contains a {accessorBody.GetType().Name} which is not supported. FieldIdentifier only supports simple member accessors (fields, properties) of an object.");
			}

			// Identify the field name. We don't mind whether it's a property or field, or even something else.
			memberName = memberExpression.Member.Name;

			// Get a reference to the model object
			// i.e., given an value like "(something).MemberName", determine the runtime value of "(something)",
			switch (memberExpression.Expression)
			{
				case ConstantExpression constantExpression:
					getOwningObjectFunc = x => constantExpression.Value;
					break;
				default:
					// It would be great to cache this somehow, but it's unclear there's a reasonable way to do
					// so, given that it embeds captured values such as "this". We could consider special-casing
					// for "() => something.Member" and building a cache keyed by "something.GetType()" with values
					// of type Func<object, object> so we can cheaply map from "something" to "something.Member".
					var modelLambda = Expression.Lambda(memberExpression.Expression, accessor.Parameters);
					var modelLambdaCompiled = (Func<TSource, object>)modelLambda.Compile();
					getOwningObjectFunc = modelLambdaCompiled;
					break;
			}
		}
	}
}
