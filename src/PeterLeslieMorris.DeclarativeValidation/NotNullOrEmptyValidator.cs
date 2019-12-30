using System;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class NotNullOrEmptyValidator
	{
		public static ClassValidationBuilder<TClass> NotNullOrEmpty<TClass>(
			this ClassValidationBuilder<TClass> builder,
			Expression<Func<TClass, string>> member)
			where TClass : class
			=> builder.AddJsonDefinition($"notNullOrEmpty", $"{{\"member\":\"{member.GetPath()}\"}}");
	}
}
