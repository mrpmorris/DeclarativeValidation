using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IValidator<TClass>
	{
		Task ValidateAsync(IValidationContext context, TClass obj);
	}
}
