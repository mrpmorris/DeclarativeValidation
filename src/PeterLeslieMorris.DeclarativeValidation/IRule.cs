using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IRule
	{
		string ToJson() => "";
		Task ValidateAsync(ValidationContext context);
	}
}
