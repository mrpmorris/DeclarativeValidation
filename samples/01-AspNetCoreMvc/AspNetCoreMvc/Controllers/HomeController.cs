using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCoreMvc.Models;
using System;
using AspNetCoreMvc.ModelValidators;
using PeterLeslieMorris.DeclarativeValidation;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace AspNetCoreMvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> Logger;
		private readonly IServiceProvider ServiceProvider;

		public HomeController(
			ILogger<HomeController> logger,
			IServiceProvider serviceProvider)
		{
			Logger = logger;
			ServiceProvider = serviceProvider;
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
			var cmv = new ClassMemberValidator<Person, string>(x => x.Address.Country.Code);
			cmv.AddValidatorFactory((sp) => new NotNullValidator<string>());
			await (cmv as IValidator<Person>).ValidateAsync(null, person);
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

	public class NotNullValidator<TValue> : IValueValidator<TValue>
		where TValue : class
	{
		Task<bool> IValueValidator<TValue>.IsValidAsync(TValue value) =>
			Task.FromResult(value != null);
	}
}
