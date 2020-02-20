﻿using System;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public class ClassConditionalValidator<TParentClass, TSubClass> : ClassValidator<TSubClass>, IValidator<TParentClass>
	{
		private readonly ClassMemberValidator<TParentClass, TSubClass> ConditionEvaluator;

		public ClassConditionalValidator(
			ClassMemberValidator<TParentClass, TSubClass> conditionEvaluator)
		{
			ConditionEvaluator = conditionEvaluator;
		}

		async Task<bool> IValidator<TParentClass>.ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			TParentClass parent)
		{
			bool conditionMet = 
				await (ConditionEvaluator as IValidator<TParentClass>).ValidateAsync(
					serviceProvider,
					context, parent);
			if (!conditionMet)
				return true;
			return await base.ValidateAsync(serviceProvider, context, ConditionEvaluator.GetValue(parent));
		}
	}
}
