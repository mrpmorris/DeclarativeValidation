using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreMvc.Models;
using PeterLeslieMorris.DeclarativeValidation;
using AspNetCoreMvc.Extensions;
using System.Text.Json;

namespace AspNetCoreMvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly IValidationService ValidationService;

		public HomeController(IValidationService validationService)
		{
			ValidationService = validationService;
		}

		public async Task<IActionResult> Index()
		{
			var person = new Person
			{
				Salutation = "X",
				GivenName = "Peter",
				FamilyName = "Morris",
				EmailAddress = "me@home.com",
				Address = new Address
				{
					Lines = new string[] { "1", "2", "3", "4" },
					Country = new Country
					{
						Code = null,
						Name = null
					}
				},
				OtherAddresses = new Address[]
				{
					new Address
					{
						Area = "Okay",
						Lines = new string[]
						{
							"a",
							null,
							null,
							"d"
						}
				 },
					new Address
					{
						Area = "Also okay",
						Lines = new string[]
						{
							null,
							"b",
							"c",
							null
						}
					},
					new Address(),
					null,
					new Address()
				}
			};

			ValidationContext context = await ValidationService.ValidateAsync(person);
			context.AddErrorsToController(this);
			ViewData["All errors"] = JsonSerializer.Serialize(context.Errors, new JsonSerializerOptions { WriteIndented = true });

			return View(person);
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
