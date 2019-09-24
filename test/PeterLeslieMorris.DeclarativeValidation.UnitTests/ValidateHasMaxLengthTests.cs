using System.Threading.Tasks;
using Xunit;

namespace PeterLeslieMorris.DeclarativeValidation.UnitTests
{
	public class ValidateHasMaxLengthTests : ValidateMemberBaseTests<int[]>
	{
		[Fact]
		public async Task IsValidAsync_ReturnsTrue_WhenLengthEqualsMaximumLength()
		{
			Validator.HasMaxLength(3);

			var value = new int[] { 1, 2, 3 };
			foreach (IRuleEvaluator<int[]> evaluator in RuleEvaluators)
				Assert.True(await evaluator.IsValidAsync(value));
		}

		[Fact]
		public async Task IsValidAsync_ReturnsTrue_WhenShorterThanMaximumLength()
		{
			Validator.HasMaxLength(3);

			var value = new int[] { 1, 2 };
			foreach (IRuleEvaluator<int[]> evaluator in RuleEvaluators)
				Assert.True(await evaluator.IsValidAsync(value));
		}

		[Fact]
		public async Task IsValidAsync_ReturnsFalse_WhenLongerThanMaximumLength()
		{
			Validator.HasMaxLength(3);

			var value = new int[] { 1, 2, 3, 4 };
			foreach (IRuleEvaluator<int[]> evaluator in RuleEvaluators)
				Assert.False(await evaluator.IsValidAsync(value));
		}
	}
}
