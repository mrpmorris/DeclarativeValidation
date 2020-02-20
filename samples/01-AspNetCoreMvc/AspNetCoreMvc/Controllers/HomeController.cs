using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCoreMvc.Models;
using PeterLeslieMorris.DeclarativeValidation;
using System.Collections;
using System.Collections.Generic;

namespace AspNetCoreMvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> Logger;
		private readonly IClassValidatorRepository ClassValidatorRepository;

		public HomeController(
			ILogger<HomeController> logger,
			IClassValidatorRepository classValidatorRepository)
		{
			Logger = logger;
			ClassValidatorRepository = classValidatorRepository;
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
						Code = "GB",
						Name = "Great Britain"
					}
				}
			};
			
			IEnumerable<IValidator> validators = ClassValidatorRepository.GetValidators<Person>();
			bool isValid = true;
			foreach(IValidator validator in validators)
			{
				isValid &= await validator.ValidateAsync(null, null, person);
			}

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
