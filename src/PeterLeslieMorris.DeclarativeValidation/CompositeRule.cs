using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class CompositeRule : IRule
	{
		private readonly List<IRule> Rules;

		public CompositeRule(IEnumerable<IRule> rules)
		{
			Rules = new List<IRule>(rules);
		}

		public string ToJson() => "";

		public async Task ValidateAsync(ValidationContext context)
		{
			foreach (IRule rule in Rules)
			{
				await rule.ValidateAsync(context);
			}
		}
	}
}
