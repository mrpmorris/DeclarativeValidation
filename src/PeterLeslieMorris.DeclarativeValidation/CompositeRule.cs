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

		public async override IAsyncEnumerable<RuleViolation> Validate(object instance)
		{
			foreach (Rule rule in Rules)
			{
				await foreach (RuleViolation ruleViolation in rule.Validate(instance))
				{
					yield return ruleViolation;
					yield break;
				}
			}
		}
	}
}
