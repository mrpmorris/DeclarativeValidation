namespace PeterLeslieMorris.DeclarativeValidation.Builders
{
	public interface IMemberRuleBuilder : IRuleBuilder
	{
		string Member { get; }
	}

	public interface IMemberRuleBuilder<TClass, TProperty> : IMemberRuleBuilder
		where TClass: class
	{
	}
}
