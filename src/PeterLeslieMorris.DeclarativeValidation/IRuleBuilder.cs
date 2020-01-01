namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IRuleBuilder
	{
		void Build(out string json, out Rule rule);
	}
}
