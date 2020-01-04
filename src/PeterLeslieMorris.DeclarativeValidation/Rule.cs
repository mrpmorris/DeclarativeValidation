using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public abstract class Rule
	{
		public abstract string ToJson();
		public abstract IAsyncEnumerable<RuleViolation> Validate(object instance);
	}
}
