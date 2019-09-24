using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	internal class WhenValidator<TClass, TMember> : ClassValidator<TClass>
	{
		private readonly ClassMemberValidator<TClass, TMember> ConditionEvaluator;

		public WhenValidator(
			ClassMemberValidator<TClass, TMember> conditionEvaluator)
		{
			ConditionEvaluator = conditionEvaluator;
		}

		protected override async Task<bool> ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			string[] memberPathSoFar,
			TClass obj)
		{
			bool conditionMet =
				await (ConditionEvaluator as IValidator<TClass>).ValidateAsync(
					serviceProvider,
					context,
					memberPathSoFar,
					obj);
			if (!conditionMet)
				return true;
			return await base.ValidateAsync(
				serviceProvider,
				context,
				memberPathSoFar,
				obj);
		}
	}
}
