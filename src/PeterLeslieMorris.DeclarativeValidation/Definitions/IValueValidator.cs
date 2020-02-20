using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public interface IValueValidator<TValue>
	{
		public string ErrorCode { get; }
		public string ErrorMessage { get; }
		Task<bool> IsValidAsync(TValue value);
	}
}
