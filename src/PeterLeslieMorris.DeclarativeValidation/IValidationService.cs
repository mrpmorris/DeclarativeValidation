using System.Collections.Generic;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IValidationService
	{
		ValidationContext Validate(object subject);
		//TODO: Make ValidateAsync
		void Validate(ValidationContext context);
	}
}
