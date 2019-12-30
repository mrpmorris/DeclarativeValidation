using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class MinLengthValidator
	{
		public static ClassValidationBuilder<TClass> MinLength<TClass, TProperty>(
			this ClassValidationBuilder<TClass> builder,
			Expression<Func<TClass, IEnumerable<TProperty>>> member,
			ulong minLength)
			where TClass : class
			=> builder.AddJsonDefinition($"minLength", $"{{\"member\":\"{member.GetPath()}\",\"min\":{minLength}}}");
	}
}
