using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Rules
{
	public class MaxLengthRule : IRule
	{
		public ulong Max { get; set; }

		public Task<bool> ValidateAsync(object value) =>
			Task.FromResult(true);
	}
}
