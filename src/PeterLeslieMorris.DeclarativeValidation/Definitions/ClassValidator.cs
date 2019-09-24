using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public class ClassValidator<TClass> : IValidator<TClass>
	{
		private readonly ConcurrentQueue<Func<IServiceProvider, IValidator<TClass>>> ValidatorFactories =
			new ConcurrentQueue<Func<IServiceProvider, IValidator<TClass>>>();

		Type IValidator.ClassToValidate => typeof(TClass);

		public ClassValidator<TClass> Validate => this;

		public void For<TMember>(
			Expression<Func<TClass, TMember>> member,
			Action<IClassMemberValidator<TClass, TMember>> validate)
		{
			var classMemberValidator = new ClassMemberValidator<TClass, TMember>(member);
			ValidatorFactories.Enqueue(sp => classMemberValidator);
			validate(classMemberValidator);
		}

		public void SwitchForEach<TMember>(
			Expression<Func<TClass, IEnumerable<TMember>>> member,
			Action<ClassValidator<TMember>> validate)
		{
			var subValidator = new SwitchForEachValidator<TClass, IEnumerable<TMember>, TMember>(member);
			ValidatorFactories.Enqueue(sp => subValidator);
			validate(subValidator.ElementValidator);
		}

		public void ForEachValue<TElement>(
			Expression<Func<TClass, IEnumerable<TElement>>> member,
			Action<IClassMemberValidator<TElement, TElement>> validate)
		{
			var subValidator = new ForEachValueValidator<TClass, TElement>(member);
			ValidatorFactories.Enqueue(sp => subValidator);
			validate(subValidator.ElementValidator);
		}

		public void SwitchWhen<TMember>(
			Expression<Func<TClass, TMember>> member,
			Action<IClassMemberValidator<TClass, TMember>> condition,
			Action<ClassValidator<TMember>> validate)
		{
			var conditionEvaluator = new ClassMemberValidator<TClass, TMember>(member);
			condition(conditionEvaluator);
			var subValidator = new SwitchWhenValidator<TClass, TMember>(conditionEvaluator);
			ValidatorFactories.Enqueue(sp => subValidator);
			validate(subValidator);
		}

		public void When<TMember>(
			Expression<Func<TClass, TMember>> member,
			Action<IClassMemberValidator<TClass, TMember>> condition,
			Action<ClassValidator<TClass>> validate)
		{
			var conditionEvaluator = new ClassMemberValidator<TClass, TMember>(member);
			condition(conditionEvaluator);
			var subValidator = new WhenValidator<TClass, TMember>(conditionEvaluator);
			ValidatorFactories.Enqueue(sp => subValidator);
			validate(subValidator);
		}

		public void AddValidatorFactory(Func<IServiceProvider, IValidator<TClass>> validatorFactory)
		{
			if (validatorFactory == null)
				throw new ArgumentNullException(nameof(validatorFactory));
			ValidatorFactories.Enqueue(validatorFactory);
		}

		Task<bool> IValidator.ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			string[] memberPathSoFar,
			object obj)
			=> ValidateAsync(
					serviceProvider,
					context,
					memberPathSoFar,
					(TClass)obj);

		protected virtual async Task<bool> ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			string[] memberPathSoFar,
			TClass obj)
		{
			bool isValid = true;
			foreach (var validatorFactory in ValidatorFactories)
			{
				var validator = validatorFactory(serviceProvider);
				isValid &= await validator.ValidateAsync(
					serviceProvider,
					context,
					memberPathSoFar,
					obj);
			}
			return isValid;
		}

		Task<bool> IValidator<TClass>.ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			string[] memberPathSoFar,
			TClass obj)
			=> ValidateAsync(serviceProvider, context, memberPathSoFar, obj);
	}
}
