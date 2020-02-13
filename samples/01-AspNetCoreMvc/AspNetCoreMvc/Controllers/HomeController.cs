using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCoreMvc.Models;
using PeterLeslieMorris.DeclarativeValidation;
using System.Collections.Generic;

namespace AspNetCoreMvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> Logger;
		private readonly IValidationService ValidationService;

		public HomeController(ILogger<HomeController> logger, IValidationService validationService)
		{
			Logger = logger;
			ValidationService = validationService;
		}

		public async Task<IActionResult> Index()
		{
			var person = new Person();
			var context = new ValidationContext();
			context.MemberValidationStarted += (_, m) => Debug.WriteLine("Started validation for " + m);
			context.MemberValidationEnded += (_, m) => Debug.WriteLine("Ended validation for " + m);
			context.AllValidationsEnded += (_, m) => Debug.WriteLine("All validation complete");

			IEnumerable<RuleViolation> ruleViolations = await ValidationService.ValidateAsync(context, person);
			foreach (RuleViolation ruleViolation in ruleViolations)
				ModelState.AddModelError(ruleViolation.MemberPath, ruleViolation.ErrorMessage);
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
