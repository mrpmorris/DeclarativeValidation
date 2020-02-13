using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Rules
{
	public class MinLengthRule : IRule
	{
		public ulong Min { get; set; }

		public Task<bool> ValidateAsync(object value) =>
			Task.FromResult(true);
	}
}
