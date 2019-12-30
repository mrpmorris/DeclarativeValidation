using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class IsNullValidator
	{
		public static ClassValidationBuilder<TClass> IsNull<TClass, TProperty>(
			this ClassValidationBuilder<TClass> builder,
			Expression<Func<TClass, TProperty>> member)
			where TClass : class
			where TProperty : class
			=> builder.AddJsonDefinition($"isNull", $"{{\"member\":\"{member.GetPath()}\"}}");

		public static ClassValidationBuilder<TClass> IsNull<TClass, TProperty>(
			this ClassValidationBuilder<TClass> builder,
			Expression<Func<TClass, TProperty?>> member)
			where TClass : class
			where TProperty : struct
			=> builder.AddJsonDefinition($"isNull", $"{{\"member\":\"{member.GetPath()}\"}}");
	}
}
