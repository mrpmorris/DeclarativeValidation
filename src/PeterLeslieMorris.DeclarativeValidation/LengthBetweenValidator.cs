using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public static class LengthBetweenValidator
	{
		public static ClassValidationBuilder<TClass> LengthBetween<TClass, TProperty>(
			this ClassValidationBuilder<TClass> builder,
			Expression<Func<TClass, IEnumerable<TProperty>>> member,
			ulong minLength,
			ulong maxLength)
			where TClass : class
		{
			if (minLength > maxLength)
				throw new ArgumentOutOfRangeException(nameof(minLength), $"Cannot be greater than {nameof(maxLength)}");

			return builder.AddJsonDefinition($"lengthBetween", $"{{\"member\":\"{member.GetPath()}\",\"min\":{minLength},\"max\":{maxLength}}}");
		}
	}
}
