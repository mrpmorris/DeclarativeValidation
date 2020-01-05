using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class CompositeRule : Rule
	{
		private readonly List<Rule> Rules;

		public CompositeRule(IEnumerable<Rule> rules)
		{
			Rules = new List<Rule>(rules);
		}

		public override string ToJson() => "";

		public async override Task ValidateAsync(ValidationContext context)
		{
			foreach (Rule rule in Rules)
			{
				await rule.ValidateAsync(context);
			}
		}
	}
}
