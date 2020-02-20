﻿using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public abstract class ClassValidator<TClass> : IValidator<TClass>
	{
		Task IValidator<TClass>.ValidateAsync(
			IValidationContext context,
			TClass obj)
		{
			throw new System.NotImplementedException();
		}
	}
}