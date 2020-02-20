using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public interface IValueValidator<TValue>
	{
		Task<bool> IsValidAsync(TValue value);
	}
}
