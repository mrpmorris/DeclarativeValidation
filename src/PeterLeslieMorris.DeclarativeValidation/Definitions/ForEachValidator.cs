using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	internal class ForEachValidator<TParentClass, TElementEnumerable, TElement> : ClassValidator<TParentClass>, IValidator<TParentClass>
		where TElementEnumerable: IEnumerable<TElement>
	{
		private readonly Func<TParentClass, IEnumerable<TElement>> GetElements;
		private readonly Expression<Func<TParentClass, IEnumerable<TElement>>> Member;
		internal readonly ClassValidator<TElement> ElementValidator;

		public ForEachValidator(
			Expression<Func<TParentClass, IEnumerable<TElement>>> member)
		{
			Member = member;
			GetElements = member.Compile();
			ElementValidator = new ClassValidator<TElement>();
		}

		protected override async Task<bool> ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			string[] memberPathSoFar,
			TParentClass obj)
		{
			bool isValid = true;
			int elementIndex = -1;
			IEnumerable<TElement> elements = GetElements(obj);
			foreach(TElement element in elements)
			{
				elementIndex++;
				isValid &= await (ElementValidator as IValidator<TElement>)
					.ValidateAsync(serviceProvider, context, memberPathSoFar, element);
			}
			return isValid;
		}
	}
}
