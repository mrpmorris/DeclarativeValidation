using System.Threading.Tasks;
using Xunit;

namespace PeterLeslieMorris.DeclarativeValidation.UnitTests
{
	public class NotNullValidatorTests
	{
		[Fact]
		public async Task IsValidAsync_ReturnsTrue_WhenValueIsNotNull()
		{
			var validator = new NotNullValidator<string>();
			bool isValid = await validator.IsValidAsync("Hello");
			Assert.True(isValid);
		}

		[Fact]
		public async Task IsValidAsync_ReturnsFalse_WhenValueIsNull()
		{
			var validator = new NotNullValidator<string>();
			bool isValid = await validator.IsValidAsync(null);
			Assert.False(isValid);
		}
	}
}
