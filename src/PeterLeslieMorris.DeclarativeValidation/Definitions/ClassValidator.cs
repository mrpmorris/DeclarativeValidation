using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public abstract class ClassValidator<TClass> : IValidator<TClass>
	{
		private readonly ConcurrentQueue<IValidator<TClass>> Validators =
			new ConcurrentQueue<IValidator<TClass>>();

		public void For<TMember>(
			Expression<Func<TClass, TMember>> member,
			Action<ClassMemberValidator<TClass, TMember>> validate)
		{
			var classMemberValidator = new ClassMemberValidator<TClass, TMember>(member);
			Validators.Enqueue(classMemberValidator);
			validate(classMemberValidator);
		}

		public void When<TMember>(
			Expression<Func<TClass, TMember>> member,
			Action<ClassMemberValidator<TClass, TMember>> condition,
			Action<ClassValidator<TMember>> validate)
		{
		}

		async Task IValidator<TClass>.ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			TClass obj)
		{
			foreach (var validator in Validators)
				await validator.ValidateAsync(serviceProvider, context, obj);
		}
	}
}
