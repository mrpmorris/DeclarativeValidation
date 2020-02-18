using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class MinimumLengthRule<TMember> : Rule<TMember>
		where TMember: IEnumerable
	{
		public uint MinimumLength { get; internal set; }

		public override Task<bool> IsValidAsync(TMember value) =>
			Task.FromResult(value != null && value.Cast<object>().Count() >= MinimumLength);
	}
}
