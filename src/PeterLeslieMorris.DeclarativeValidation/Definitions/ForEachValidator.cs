using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Extensions;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	internal class ForEachValidator<TParentClass, TElementEnumerable, TElement> : ClassValidator<TParentClass>, IValidator<TParentClass>
		where TElementEnumerable: IEnumerable<TElement>
	{
		private readonly string MemberName;
		private readonly string MemberPath;
		private readonly Func<TParentClass, IEnumerable<TElement>> GetElements;
		private readonly Expression<Func<TParentClass, IEnumerable<TElement>>> Member;
		internal readonly ClassValidator<TElement> ElementValidator;

		public ForEachValidator(
			Expression<Func<TParentClass, IEnumerable<TElement>>> member)
		{
			Member = member;
			(MemberName, MemberPath) = member.GetMemberNameAndPath();
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
			if (elements == null)
				return true;

			string[] newMemberPath =
				(new List<string>(memberPathSoFar).Append(MemberPath)).ToArray();

			int lastPathIndex = newMemberPath.Length - 1;
			string lastPathValue = newMemberPath[lastPathIndex];
			foreach(TElement element in elements)
			{
				elementIndex++;
				newMemberPath[lastPathIndex] = $"{lastPathValue}[{elementIndex}]";
				isValid &= await (ElementValidator as IValidator<TElement>)
					.ValidateAsync(serviceProvider, context, newMemberPath, element);
			}
			return isValid;
		}
	}
}
