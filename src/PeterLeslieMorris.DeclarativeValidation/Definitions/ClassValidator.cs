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

		public void SwitchWhen<TMember>(
			Expression<Func<TClass, TMember>> member,
			Action<ClassMemberValidator<TClass, TMember>> condition,
			Action<ClassValidator<TMember>> validate)
		{
			var conditionEvaluator = new ClassMemberValidator<TClass, TMember>(member);
			condition(conditionEvaluator);
			var subValidator = new SwitchWhenValidator<TClass, TMember>(conditionEvaluator);
			Validators.Enqueue(subValidator);
			validate(subValidator);
		}

		public void When<TMember>(
			Expression<Func<TClass, TMember>> member,
			Action<ClassMemberValidator<TClass, TMember>> condition,
			Action<ClassValidator<TClass>> validate)
		{
			var conditionEvaluator = new ClassMemberValidator<TClass, TMember>(member);
			condition(conditionEvaluator);
			var subValidator = new WhenValidator<TClass, TMember>(conditionEvaluator);
			Validators.Enqueue(subValidator);
			validate(subValidator);
		}

		protected virtual async Task<bool> ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			TClass obj)
		{
			bool isValid = true;
			foreach (var validator in Validators)
				isValid &= await validator.ValidateAsync(serviceProvider, context, obj);
			return isValid;
		}

		Task<bool> IValidator<TClass>.ValidateAsync(IServiceProvider serviceProvider, IValidationContext context, TClass obj) =>
			ValidateAsync(serviceProvider, context, obj);
	}
}
