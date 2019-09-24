using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PeterLeslieMorris.DeclarativeValidation.Definitions;
using PeterLeslieMorris.DeclarativeValidation.DependencyInjection;

namespace PeterLeslieMorris.DeclarativeValidation.UnitTests
{
	public abstract class ValidateMemberBaseTests<TMember>
	{
		protected IClassMemberValidator<object, TMember> Validator => MockClassMemberValidator.Object;
		protected readonly IServiceProvider ServiceProvider;
		protected Mock<IClassMemberValidator<object, TMember>> MockClassMemberValidator;
		protected readonly List<Func<IServiceProvider, IRuleEvaluator<TMember>>> RuleEvaluatorFactories;

		public ValidateMemberBaseTests()
		{
			MockClassMemberValidator = CreateMockClassMemberValidator(MockBehavior.Strict);
			RuleEvaluatorFactories = new List<Func<IServiceProvider, IRuleEvaluator<TMember>>>();
			SetUpMockClassMemberValidator();

			var services = new ServiceCollection();
			services.AddDeclarativeValidation(typeof(ValidateIsNotNullExtension).Assembly, typeof(ValidateIsNullTests).Assembly);
			ServiceProvider = services.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
		}

		protected virtual void RegisterServices(IServiceCollection services) { }

		protected IEnumerable<IRuleEvaluator<TMember>> RuleEvaluators =>
			RuleEvaluatorFactories.Select(x => x(ServiceProvider));

		protected virtual Mock<IClassMemberValidator<object, TMember>> CreateMockClassMemberValidator(MockBehavior mockBehavior) =>
			new Mock<IClassMemberValidator<object, TMember>>(mockBehavior);

		protected virtual void SetUpMockClassMemberValidator()
		{
			MockClassMemberValidator
				.Setup(x => x.AddRuleEvaluatorFactory(It.IsAny<Func<IServiceProvider, IRuleEvaluator<TMember>>>()))
				.Callback<Func<IServiceProvider, IRuleEvaluator<TMember>>>(x => RuleEvaluatorFactories.Add(x));
		}
	}
}
