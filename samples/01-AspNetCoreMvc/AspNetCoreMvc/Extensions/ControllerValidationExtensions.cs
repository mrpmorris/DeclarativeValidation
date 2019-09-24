using Microsoft.AspNetCore.Mvc;
using PeterLeslieMorris.DeclarativeValidation;

namespace AspNetCoreMvc.Extensions
{
	public static class ControllerValidationExtensions
	{
		public static void AddErrorsToController(this ValidationContext context, Controller controller)
		{
			foreach (ValidationError error in context.Errors)
				controller.ModelState.AddModelError(error.MemberPath, error.ErrorMessage);
		}
	}
}
