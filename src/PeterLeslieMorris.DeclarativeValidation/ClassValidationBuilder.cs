using System.Collections.Generic;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class ClassValidationBuilder<TClass> where TClass : class
	{
		private readonly List<string> Definitions = new List<string>();
		public ClassValidationBuilder<TClass> AddJsonDefinition(string validatorId, string json)
		{
			Definitions.Add($"{{\"{validatorId}\":{json}}}");
			return this;
		}

		public string Build() => "{\"validation\": [" + string.Join(",", Definitions) + "]}";
	}
}
