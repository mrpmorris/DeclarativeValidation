﻿using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCoreMvc.Models;
using System;
using AspNetCoreMvc.ModelValidators;
using PeterLeslieMorris.DeclarativeValidation.Definitions;
using PeterLeslieMorris.DeclarativeValidation;

namespace AspNetCoreMvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> Logger;
		private readonly IServiceProvider ServiceProvider;

		public HomeController(ILogger<HomeController> logger, IServiceProvider serviceProvider)
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
					Area = "Ha"
				}
			};
			var validator = new PersonValidator();
			foreach(var memberAndRuleFactories in validator.GetRuleFactories())
			{
				object memberValue = memberAndRuleFactories.GetMemberValue(person);
				foreach(IMemberRuleFactory ruleFactory in memberAndRuleFactories.RuleFactories)
				{
					var rule = ruleFactory.CreateRule(ServiceProvider);
					bool isValid = await rule.IsValidAsync(memberValue);
					if (!isValid)
						break;
				}
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
