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

		public override async Task<bool> ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			TClass obj)
		{
			bool conditionMet =
				await (ConditionEvaluator as IValidator<TClass>).ValidateAsync(
					serviceProvider,
					context, obj);
			if (!conditionMet)
				return true;
			return await base.ValidateAsync(serviceProvider, context, obj);
		}
	}

}
