using System.Threading.Tasks;
using Xunit;

namespace PeterLeslieMorris.DeclarativeValidation.UnitTests
{
	public class ValidateIsNotNullTests : ValidateMemberBaseTests<string>
	{
		[Fact]
		public async Task IsValidAsync_ReturnsTrue_WhenHasValue()
		{
			Validator.IsNotNull();
			foreach (IRuleEvaluator<string> evaluator in RuleEvaluators)
				Assert.True(await evaluator.IsValidAsync("Hello"));
		}

		[Fact]
		public async Task IsValidAsync_ReturnsFalse_WhenValueIsNull()
		{
			Validator.IsNotNull();
			foreach (IRuleEvaluator<string> evaluator in RuleEvaluators)
				Assert.False(await evaluator.IsValidAsync(null));
		}
	}
}
