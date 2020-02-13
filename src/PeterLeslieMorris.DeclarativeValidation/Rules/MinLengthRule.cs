using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Rules
{
	public class MinLengthRule : IRule
	{
		public ulong Min { get; set; }

		public Task<bool> ValidateAsync(ValidationContext context, object value) =>
			Task.FromResult(true);
	}
}
