using System;
using System.Linq;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	internal class SwitchWhenValidator<TParentClass, TSubClass> : ClassValidator<TSubClass>, IValidator<TParentClass>
	{
		private readonly ClassMemberValidator<TParentClass, TSubClass> ConditionEvaluator;

		public SwitchWhenValidator(
			ClassMemberValidator<TParentClass, TSubClass> conditionEvaluator)
		{
			ConditionEvaluator = conditionEvaluator;
		}

		async Task<bool> IValidator<TParentClass>.ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			string[] memberPathSoFar,
			TParentClass parent)
		{
			bool conditionMet = 
				await (ConditionEvaluator as IValidator<TParentClass>).ValidateAsync(
					serviceProvider,
					context,
					memberPathSoFar,
					parent);
			if (!conditionMet)
				return true;
			return await base.ValidateAsync(
				serviceProvider,
				context,
				memberPathSoFar.Append(ConditionEvaluator.MemberPath).ToArray(),
				ConditionEvaluator.GetValue(parent));
		}
	}
}
