using System.Threading.Tasks;
using Xunit;

namespace PeterLeslieMorris.DeclarativeValidation.UnitTests
{
	public class ValidateIsInternetEmailAddressTests : ValidateMemberBaseTests<string>
	{
		[Fact]
		public async Task IsValidAsync_ReturnsTrue_WhenValueIsNull()
		{
			Validator.IsValidInternetEmailAddress();
			foreach (IRuleEvaluator<string> evaluator in RuleEvaluators)
				Assert.True(await evaluator.IsValidAsync(null));
		}

		[Theory]
		[InlineData("me@home.com")]
		[InlineData("bob.monkhouse@itv.com")]
		[InlineData("andre.the.sealion@sealions.waterpark.org")]
		public async Task IsValidAsync_ReturnsTrue_WhenValueIsValid(string emailAddress)
		{
			Validator.IsValidInternetEmailAddress();
			foreach (IRuleEvaluator<string> evaluator in RuleEvaluators)
				Assert.True(await evaluator.IsValidAsync(emailAddress));
		}

		[Theory]
		[InlineData("@home.com")]
		[InlineData("bob.monkhouse")]
		[InlineData("andre@org")]
		[InlineData("andre@.org")]
		[InlineData("andre.the.sealion@sealions.waterpark..org")]
		public async Task IsValidAsync_ReturnsFalse_WhenValueIsInvalid(string emailAddress)
		{
			Validator.IsValidInternetEmailAddress();
			foreach (IRuleEvaluator<string> evaluator in RuleEvaluators)
				Assert.False(await evaluator.IsValidAsync(emailAddress));
		}
	}
}
