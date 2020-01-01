namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IMemberRuleBuilder : IRuleBuilder
	{
		string Member { get; }
	}

	public interface IMemberRuleBuilder<TClass, TProperty>
		where TClass: class
	{
	}
}
