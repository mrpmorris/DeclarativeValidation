using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PeterLeslieMorris.DeclarativeValidation.Definitions;
using PeterLeslieMorris.DeclarativeValidation.Extensions;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public abstract class ClassValidator<TClass>
	{
		private readonly ConcurrentDictionary<string, MemberAndRuleFactories> RuleFactoriesByMemberPath;

		public ClassValidator()
		{
			RuleFactoriesByMemberPath = new ConcurrentDictionary<string, MemberAndRuleFactories>();
		}

		public IEnumerable<MemberAndRuleFactories> GetRuleFactories() =>
			RuleFactoriesByMemberPath.Values;

		public void For<TMember>(
			Expression<Func<TClass, TMember>> member,
			Action<MemberRuleBuilder<TClass, TMember>> buildRuleFactories)
		{
			var memberRuleBuilder = new MemberRuleBuilder<TClass, TMember>(this);
			buildRuleFactories(memberRuleBuilder);

			var memberRuleFactories = memberRuleBuilder.GetMemberRuleFactories();

			string memberPath = member.GetMemberPath();
			RuleFactoriesByMemberPath.AddOrUpdate(
				key: memberPath,
				addValue: new MemberAndRuleFactories(memberPath, memberRuleFactories),
				updateValueFactory: (key, validators) =>
					new MemberAndRuleFactories(
						memberPath,
						validators.RuleFactories.Union(memberRuleFactories).ToArray()));
		}
	}
}
