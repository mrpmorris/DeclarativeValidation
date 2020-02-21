using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

		Type IValidator.ClassToValidate => typeof(TClass);
		internal Func<TClass, TMember> GetValue { get; }
		private ConcurrentQueue<Func<IServiceProvider, IValueValidator<TMember>>> ValidatorFactories;
		private Lazy<Func<TClass, object>> LazyGetOwner { get; }
		private object GetOwner(TClass source) => LazyGetOwner.Value(source);

		public ClassMemberValidator(Expression<Func<TClass, TMember>> member)
		{
			if (member == null)
				throw new ArgumentNullException(nameof(member));

			ValidatorFactories = new ConcurrentQueue<Func<IServiceProvider, IValueValidator<TMember>>>();
			GetValue = member.Compile();
			MemberPath = member.GetMemberPath();

			int lastDotIndex = MemberPath.LastIndexOf('.');
			MemberName = lastDotIndex == -1
				? MemberPath
				: MemberPath.Remove(0, lastDotIndex + 1);

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

		Task<bool> IValidator.ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			string[] memberPathSoFar,
			object obj)
			=> (this as IValidator<TClass>)
			.ValidateAsync(serviceProvider, context, memberPathSoFar, obj);

		async Task<bool> IValidator<TClass>.ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			string[] memberPathSoFar,
			TClass obj)
		{
			TMember memberValue = GetValue(obj);
			foreach (var validatorFactory in ValidatorFactories)
			{
				IValueValidator<TMember> validator =
					validatorFactory(serviceProvider);
				bool isValid = await validator.IsValidAsync(memberValue);
				if (!isValid && context != null)
				{
					string memberPath =
						string.Join(
							'.',
							new List<string>(memberPathSoFar).Append(MemberPath));

					var validationError = new ValidationError(
						memberName: MemberName,
						memberPath: memberPath,
						errorCode: validator.ErrorCode,
						errorMessage: validator.ErrorMessage,
						() => new MemberIdentifier(GetOwner(obj), MemberName));
					context.AddError(validationError);
					return false;
				};
			}
			return true;
		}
	}
}
