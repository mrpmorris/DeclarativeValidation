using System.Collections.Generic;

namespace PeterLeslieMorris.DeclarativeValidation.Definitions
{
	public class MemberRuleBuilder<TClass, TMember>
	{
		private readonly ClassValidator<TClass> ClassFactory;
		private readonly List<IMemberRuleFactory> RuleFactories;

		public MemberRuleBuilder(ClassValidator<TClass> classFactory)
		{
			ClassFactory = classFactory;
			RuleFactories = new List<IMemberRuleFactory>();
		}

		public void AddRuleFactory(IMemberRuleFactory ruleFactory)
		{
			RuleFactories.Add(ruleFactory);
		}

		public IEnumerable<IMemberRuleFactory> GetMemberRuleFactories() =>
			RuleFactories;
	}
}
