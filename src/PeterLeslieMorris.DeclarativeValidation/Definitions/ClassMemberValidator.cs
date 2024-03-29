﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Extensions;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public interface IClassMemberValidator<TClass, TMember>
	{
		void AddRuleEvaluatorFactory(Func<IServiceProvider, IRuleEvaluator<TMember>> factory);
	}

	internal class ClassMemberValidator<TClass, TMember> : IValidator<TClass>, IClassMemberValidator<TClass, TMember>
	{
		public string MemberName { get; }
		public string MemberPath { get; }

		Type IValidator.ClassToValidate => typeof(TClass);
		internal Func<TClass, TMember> GetValue { get; }
		private ConcurrentQueue<Func<IServiceProvider, IRuleEvaluator<TMember>>> RuleEvaluatorFactories;
		private Lazy<Func<TClass, object>> LazyGetOwner { get; }
		private object GetOwner(TClass source) => LazyGetOwner.Value(source);

		public ClassMemberValidator(Expression<Func<TClass, TMember>> member)
		{
			if (member == null)
				throw new ArgumentNullException(nameof(member));

			RuleEvaluatorFactories = new ConcurrentQueue<Func<IServiceProvider, IRuleEvaluator<TMember>>>();
			GetValue = member.Compile();
			(MemberName, MemberPath) = member.GetMemberNameAndPath();

			LazyGetOwner = new Lazy<Func<TClass, object>>(
				() =>
				{
					member.ParseAccessor(
						out Func<TClass, object> getOwner,
						out string memberName);
					return getOwner;
				});
		}

		public void AddRuleEvaluatorFactory(Func<IServiceProvider, IRuleEvaluator<TMember>> factory)
		{
			if (factory == null)
				throw new ArgumentNullException(nameof(factory));
			RuleEvaluatorFactories.Enqueue(factory);
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
			foreach (var ruleEvaluatorFactory in RuleEvaluatorFactories)
			{
				IRuleEvaluator<TMember> ruleEvaluator = ruleEvaluatorFactory(serviceProvider);
				bool isValid = await ruleEvaluator.IsValidAsync(memberValue);
				if (!isValid && context != null)
				{
					IEnumerable<string> newMemberPathSoFar =
						memberPathSoFar;
					if (!string.IsNullOrEmpty(MemberPath))
						newMemberPathSoFar = newMemberPathSoFar.Append(MemberPath);

					string memberPath = string.Join('.', newMemberPathSoFar);

					var validationError = new ValidationError(
						memberName: MemberName,
						memberPath: memberPath,
						errorCode: ruleEvaluator.ErrorCode,
						errorMessage: ruleEvaluator.GetErrorMessage(memberValue),
						() => new MemberIdentifier(GetOwner(obj), MemberName));
					context.AddError(validationError);
					return false;
				};
			}
			return true;
		}
	}
}
