using System;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IRuleEvaluator<TValue>
	{
		public string ErrorCode { get; }
		public Func<TValue, string> GetErrorMessage { get; }
		Task<bool> IsValidAsync(TValue value);
	}

	public class RuleEvaluator<TValue> : IRuleEvaluator<TValue>
	{
		public string ErrorCode { get; }
		public Func<TValue, string> GetErrorMessage { get; }
		public Task<bool> IsValidAsync(TValue value) => OnIsValidAsync(value);

		private readonly Func<TValue, Task<bool>> OnIsValidAsync;

		public RuleEvaluator(
			string errorCode,
			Func<TValue, string> getErrorMessage,
			Func<TValue, Task<bool>> isValidAsync)
		{
			if (getErrorMessage == null)
				throw new ArgumentNullException(nameof(getErrorMessage));
			if (isValidAsync == null)
				throw new ArgumentNullException(nameof(isValidAsync));

			ErrorCode = errorCode;
			GetErrorMessage = getErrorMessage;
			OnIsValidAsync = isValidAsync;
		}
	}
}
