using System.Threading.Tasks;
using PeterLeslieMorris.DeclarativeValidation.Definitions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class NotNullRule<TMember> : Rule<TMember>
		where TMember : class
	{
		public override Task<bool> IsValidAsync(TMember value) =>
			Task.FromResult(value != null);
	}
}
