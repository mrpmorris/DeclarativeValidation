using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class CompositeParallelRule : IRule
	{
		private readonly List<IRule> Rules;

		public CompositeParallelRule(IEnumerable<IRule> rules)
		{
			Rules = new List<IRule>(rules);
		}

		public async Task<bool> ValidateAsync(object value)
		{
			var allTasks = Rules.Select(x => x.ValidateAsync(value));
			await Task.WhenAll(allTasks);
			return allTasks.All(x => x.Result);
		}
	}
}
