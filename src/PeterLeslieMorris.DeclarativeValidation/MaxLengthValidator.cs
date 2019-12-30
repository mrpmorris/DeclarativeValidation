using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class MaxLengthValidator
	{
		public static ClassValidationBuilder<TClass> MaxLength<TClass, TProperty>(
			this ClassValidationBuilder<TClass> builder,
			Expression<Func<TClass, IEnumerable<TProperty>>> member,
			ulong maxLength)
			where TClass : class
			=> builder.AddJsonDefinition($"maxLength", $"{{\"member\":\"{member.GetPath()}\",\"max\":{maxLength}}}");
	}
}
