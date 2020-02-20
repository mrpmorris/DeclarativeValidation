using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Extensions;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public class ClassMemberValidator<TClass, TMember> : IValidator<TClass>
	{
		public string MemberName { get; }
		public string MemberPath { get; }

		private ConcurrentQueue<Func<IServiceProvider, IValueValidator<TMember>>> ValidatorFactories;
		private Func<TClass, TMember> GetValue { get; }
		private Lazy<Func<TClass, object>> LazyGetOwner { get; }
		private object GetOwner(TClass source) => LazyGetOwner.Value(source);

		public ClassMemberValidator(Expression<Func<TClass, TMember>> member)
		{
			if (member == null)
				throw new ArgumentNullException(nameof(member));

			ValidatorFactories = new ConcurrentQueue<Func<IServiceProvider, IValueValidator<TMember>>>();
			GetValue = member.Compile();
			MemberPath = member.GetMemberPath();
			MemberName = MemberPath.Split('.').Last();
			LazyGetOwner = new Lazy<Func<TClass, object>>(
				() =>
				{
					member.ParseAccessor(
						out Func<TClass, object> getOwner,
						out string memberName);
					return getOwner;
				});
		}

		public void AddValidatorFactory(Func<IServiceProvider, IValueValidator<TMember>> factory)
		{
			if (factory == null)
				throw new ArgumentNullException(nameof(factory));
			ValidatorFactories.Enqueue(factory);
		}

		async Task IValidator<TClass>.ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			TClass obj)
		{
			TMember memberValue = GetValue(obj);
			foreach (var validatorFactory in ValidatorFactories)
			{
				IValueValidator<TMember> validator =
					validatorFactory(serviceProvider);
				bool isValid = await validator.IsValidAsync(memberValue);
				if (!isValid)
				{
					var validationError = new ValidationError(
						memberName: MemberName,
						memberPath: MemberPath,
						errorCode: validator.ErrorCode,
						errorMessage: validator.ErrorMessage,
						() => new MemberIdentifier(GetOwner(obj), MemberName));
					break;
				};
			}
		}
	}
}
