using System.Threading.Tasks;
using Xunit;

namespace PeterLeslieMorris.DeclarativeValidation.UnitTests
{
	public class ValidateHasMinLengthTests
	{
		[Fact]
		public async Task IsValidAsync_ReturnsTrue_WhenLengthEqualsMinimumLength()
		{
			var validator = new ValidateHasMinLength<int[]>(3);
			bool isValid = await validator.IsValidAsync(new int[] { 1, 2, 3 });
			Assert.True(isValid);
		}

		[Fact]
		public async Task IsValidAsync_ReturnsTrue_WhenLongerThanMinimumLength()
		{
			var validator = new ValidateHasMinLength<int[]>(3);
			bool isValid = await validator.IsValidAsync(new int[] { 1, 2, 3, 4 });
			Assert.True(isValid);
		}

		[Fact]
		public async Task IsValidAsync_ReturnsFalse_WhenShorterThanMinimumLength()
		{
			var validator = new ValidateHasMinLength<int[]>(3);
			bool isValid = await validator.IsValidAsync(new int[] { 1, 2 });
			Assert.False(isValid);
		}

		[Fact]
		public void ErrorMessage_UsesErrorMessageFormat()
		{
			var validator = new ValidateHasMinLength<int[]>(3, errorMessageFormat: "XY{0}Z");
			Assert.Equal("XY3Z", validator.ErrorMessage);
		}
	}
}
