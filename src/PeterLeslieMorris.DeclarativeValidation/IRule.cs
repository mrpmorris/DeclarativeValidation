using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IRule
	{
		Task<bool> ValidateAsync(object value);
	}
}
