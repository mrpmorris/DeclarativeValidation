using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class CompositeParallelRule : RuleBase
	{
		private readonly List<IRule> Rules;

		public CompositeParallelRule(IEnumerable<IRule> rules) : base()
		{
			Rules = new List<IRule>(rules);
		}

		public override string ToJson() => "";

		public override Task ValidateAsync(ValidationContext context)
		{
			var allTasks = Rules.Select(x => x.ValidateAsync(context));
			return Task.WhenAll(allTasks);
		}
	}
}
