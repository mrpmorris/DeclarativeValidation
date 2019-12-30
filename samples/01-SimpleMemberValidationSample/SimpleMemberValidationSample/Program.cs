using SimpleMemberValidationSample.ValidationProfiles;
using System;

namespace SimpleMemberValidationSample
{
	class Program
	{
		static void Main(string[] args)
		{
			var profile = new PersonValidation();
			string json = Newtonsoft.Json.JsonConvert.ToString(profile);

		}
	}
}
