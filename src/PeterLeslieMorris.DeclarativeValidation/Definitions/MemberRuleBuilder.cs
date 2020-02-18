namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public class MemberRuleBuilder<TClass, TMember>
	{
		private readonly ClassValidator<TClass> ClassFactory;

		public MemberRuleBuilder(ClassValidator<TClass> classFactory)
		{
			ClassFactory = classFactory;
		}
	}
}
