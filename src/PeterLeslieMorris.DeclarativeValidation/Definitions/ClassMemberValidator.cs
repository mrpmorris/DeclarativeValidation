using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Extensions;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public class ClassMemberValidator<TClass, TMember> : IValidator<TMember>
	{
		public Func<TClass, TMember> GetValue { get; }
		public string MemberName { get; }
		public string MemberPath { get; }

		private Lazy<Func<TClass, object>> LazyGetOwner { get; }
		private Func<TClass, object> GetOwner() => LazyGetOwner.Value;

		public ClassMemberValidator(Expression<Func<TClass, TMember>> member)
		{
			if (member == null)
				throw new ArgumentNullException(nameof(member));

			GetValue = member.Compile();
			MemberPath = member.GetMemberPath();
			LazyGetOwner = new Lazy<Func<TClass, object>>(
				() =>
				{
					member.ParseAccessor(
						out Func<TClass, object> getOwner,
						out string memberName);
					return getOwner;
				});
		}
		
		Task IValidator<TMember>.ValidateAsync(
			IValidationContext context,
			TMember obj)
		{
			throw new NotImplementedException();
		}
	}
}
