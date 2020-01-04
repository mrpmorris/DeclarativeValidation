﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspNetCoreMvc.Models;
using PeterLeslieMorris.DeclarativeValidation;

namespace AspNetCoreMvc.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IValidationService ValidationService;

		public HomeController(ILogger<HomeController> logger, IValidationService validationService)
		{
			_logger = logger;
			ValidationService = validationService;
		}

		public async Task<IActionResult> Index()
		{
			var person = new Person();
			await foreach(RuleViolation ruleViolation in ValidationService.Validate(person)) {
				ModelState.AddModelError("X", "Y");
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
