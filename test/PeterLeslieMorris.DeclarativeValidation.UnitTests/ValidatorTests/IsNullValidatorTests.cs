using System.Threading.Tasks;
using Xunit;

namespace PeterLeslieMorris.DeclarativeValidation.UnitTests.ValidatorTests
{
	public class IsNullValidatorTests
	{
		[Fact]
		public async Task IsValidAsync_ReturnsTrue_WhenValueIsNull()
		{
			var validator = new IsNullValidator<string>();
			bool isValid = await validator.IsValidAsync(null);
			Assert.True(isValid);
		}

		[Fact]
		public async Task IsValidAsync_ReturnsFalse_WhenHasValue()
		{
			var validator = new IsNullValidator<string>();
			bool isValid = await validator.IsValidAsync("Hello");
			Assert.False(isValid);
		}
	}
}
