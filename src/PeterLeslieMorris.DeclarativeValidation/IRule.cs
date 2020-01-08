using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IRule
	{
		string MemberPath { get; set; }
		string ToJson() => "";
		Task ValidateAsync(ValidationContext context);
	}
}
