using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class NotNullOrWhitespaceValidator
	{
		public static ClassValidationBuilder<TClass> NotNullOrWhitespace<TClass>(
			this ClassValidationBuilder<TClass> builder,
			Expression<Func<TClass, string>> member)
			where TClass : class
			=> builder.AddJsonDefinition($"notNullOrWhitespace", $"{{\"member\":\"{member.GetPath()}\"}}");
	}
}
