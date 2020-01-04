using System.Collections.Generic;

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
	}
}
