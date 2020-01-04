using System;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation;
using SimpleMemberValidationSample.ValidationProfiles;

namespace SimpleMemberValidationSample
{
	class Program
	{
		static async Task Main(string[] args)
		{
			IValidationService validationService = new ValidationService();
			await foreach(RuleViolation violation in validationService.Validate(null))
			{
				Console.WriteLine(DateTime.Now.ToString() + " Got one");
			}
			Console.WriteLine("Done");
			Console.ReadLine();
		}
	}
}
