using System.Threading.Tasks;
using Xunit;

namespace PeterLeslieMorris.DeclarativeValidation.UnitTests
{
	public class ValidateIsNullTests : ValidateMemberBaseTests<string>
	{
		[Fact]
		public async Task IsValidAsync_ReturnsTrue_WhenValueIsNull()
		{
			Validator.IsNull();
			foreach (IRuleEvaluator<string> evaluator in RuleEvaluators)
				Assert.True(await evaluator.IsValidAsync(null));
		}

		[Fact]
		public async Task IsValidAsync_ReturnsFalse_WhenHasValue()
		{
			Validator.IsNull();
			foreach (IRuleEvaluator<string> evaluator in RuleEvaluators)
				Assert.False(await evaluator.IsValidAsync("Hello"));
		}
	}
}
