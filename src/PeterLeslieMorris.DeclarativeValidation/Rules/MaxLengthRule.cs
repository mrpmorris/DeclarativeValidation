using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Rules
{
	public class MaxLengthRule : IRule
	{
		public ulong Max { get; set; }

		public Task<bool> ValidateAsync(ValidationContext context, object value) =>
			Task.FromResult(true);
	}
}
