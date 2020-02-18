using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public interface IRule
	{
		Task<bool> IsValidAsync(object value);
	}

	public abstract class Rule<TMember> : IRule
	{
		public abstract Task<bool> IsValidAsync(TMember value);

		Task<bool> IRule.IsValidAsync(object value) =>
			IsValidAsync((TMember)value);
	}
}
