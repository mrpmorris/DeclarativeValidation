using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCoreMvc.Models;
using PeterLeslieMorris.DeclarativeValidation;
using System.Linq;

namespace AspNetCoreMvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> Logger;
		private readonly IValidationService ValidationService;

		public HomeController(
			ILogger<HomeController> logger,
			IValidationService validationService)
		{
			Logger = logger;
			ValidationService = validationService;
		}

		public async Task<IActionResult> Index()
		{
			var person = new Person
			{
				Salutation = "X",
				GivenName = "Peter",
				FamilyName = "Morris",
				EmailAddress = "me",
				Address = new Address
				{
					Country = new Country
					{
						Code = null,
						Name = "Great Britain"
					}
				}
			};

			ValidationContext context = await ValidationService.ValidateAsync(person);
			bool isValid = context.IsValid;
			MemberIdentifier mi = context.Errors.First().GetMemberIdentifier();
			
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
