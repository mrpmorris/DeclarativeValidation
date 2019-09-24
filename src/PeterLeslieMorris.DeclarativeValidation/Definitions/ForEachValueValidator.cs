using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Extensions;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	internal class ForEachValueValidator<TClass, TElement> : IValidator<TClass>, IClassMemberValidator<TClass, TElement>
	{
		private readonly string MemberPath;
		private readonly Func<TClass, IEnumerable<TElement>> GetElements;
		private readonly Expression<Func<TClass, IEnumerable<TElement>>> Member;
		private ConcurrentQueue<Func<IServiceProvider, IRuleEvaluator<TElement>>> RuleEvaluatorFactories;
		internal readonly ClassMemberValidator<TElement, TElement> ElementValidator;

		Type IValidator.ClassToValidate => typeof(TClass);

		public ForEachValueValidator(
			Expression<Func<TClass, IEnumerable<TElement>>> member)
		{
			Member = member;
			(_, MemberPath) = Member.GetMemberNameAndPath();
			GetElements = Member.Compile();
			ElementValidator = new ClassMemberValidator<TElement, TElement>(x => x);
			RuleEvaluatorFactories = new ConcurrentQueue<Func<IServiceProvider, IRuleEvaluator<TElement>>>();
		}

		Task<bool> IValidator.ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			string[] memberPathSoFar,
			object obj)
			=> (this as IValidator<TClass>)
				.ValidateAsync(
					serviceProvider,
					context,
					memberPathSoFar,
					(TClass)obj);

		async Task<bool> IValidator<TClass>.ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			string[] memberPathSoFar,
			TClass obj)
		{
			bool isValid = true;
			int elementIndex = -1;
			IEnumerable<TElement> elements = GetElements(obj);
			if (elements == null)
				return true;

			string[] newMemberPath =
				new List<string>(memberPathSoFar).Append(MemberPath).ToArray();

			int lastPathIndex = newMemberPath.Length - 1;
			string lastPathValue = newMemberPath[lastPathIndex];
			foreach (TElement element in elements)
			{
				elementIndex++;
				newMemberPath[lastPathIndex] = $"{lastPathValue}[{elementIndex}]";
				isValid &= await (ElementValidator as IValidator<TElement>)
					.ValidateAsync(serviceProvider, context, newMemberPath, element);
			}
			return isValid;
		}

		void IClassMemberValidator<TClass, TElement>.AddRuleEvaluatorFactory(Func<IServiceProvider, IRuleEvaluator<TElement>> factory)
		{
			if (factory == null)
				throw new ArgumentNullException(nameof(factory));
			RuleEvaluatorFactories.Enqueue(factory);
		}
	}
}
