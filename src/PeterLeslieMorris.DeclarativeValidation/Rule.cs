using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public abstract class Rule
	{
		public abstract string ToJson();
		public abstract Task ValidateAsync(ValidationContext context);
	}
}
