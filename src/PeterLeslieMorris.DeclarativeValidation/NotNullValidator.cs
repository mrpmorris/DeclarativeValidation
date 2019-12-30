using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class NotNullValidator
	{
		public static ClassValidationBuilder<TClass> NotNull<TClass, TProperty>(
			this ClassValidationBuilder<TClass> builder,
			Expression<Func<TClass, TProperty>> member)
			where TClass : class
			where TProperty : class
			=> builder.AddJsonDefinition($"notNull", $"{{\"member\":\"{member.GetPath()}\"}}");

		public static ClassValidationBuilder<TClass> NotNull<TClass, TProperty>(
			this ClassValidationBuilder<TClass> builder,
			Expression<Func<TClass, TProperty?>> member)
			where TClass : class
			where TProperty : struct
			=> builder.AddJsonDefinition($"notNull", $"{{\"member\":\"{member.GetPath()}\"}}");
	}
}
