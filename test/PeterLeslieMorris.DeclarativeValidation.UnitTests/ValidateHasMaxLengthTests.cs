using System.Threading.Tasks;
using Xunit;

namespace PeterLeslieMorris.DeclarativeValidation.UnitTests
{
	public class ValidateHasMaxLengthTests
	{
		[Fact]
		public async Task IsValidAsync_ReturnsTrue_WhenLengthEqualsMaximumLength()
		{
			var validator = new ValidateHasMaxLength<int[]>(3);
			bool isValid = await validator.IsValidAsync(new int[] { 1, 2, 3 });
			Assert.True(isValid);
		}

		[Fact]
		public async Task IsValidAsync_ReturnsFalse_WhenShorterThanMaximumLength()
		{
			var validator = new ValidateHasMaxLength<int[]>(3);
			bool isValid = await validator.IsValidAsync(new int[] { 1, 2 });
			Assert.True(isValid);
		}

		[Fact]
		public async Task IsValidAsync_ReturnsFalse_WhenLongerThanMaximumLength()
		{
			var validator = new ValidateHasMaxLength<int[]>(3);
			bool isValid = await validator.IsValidAsync(new int[] { 1, 2, 3, 4 });
			Assert.False(isValid);
		}

		[Fact]
		public void ErrorMessage_UsesErrorMessageFormat()
		{
			var validator = new ValidateHasMaxLength<int[]>(3, errorMessageFormat: "XY{0}Z");
			Assert.Equal("XY3Z", validator.ErrorMessage);
		}
	}
}
