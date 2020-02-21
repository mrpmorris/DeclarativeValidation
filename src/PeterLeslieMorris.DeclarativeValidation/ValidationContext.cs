using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IValidationContext
	{
		bool HasErrors { get; }
		string Scenario { get; }
		IEnumerable<string> MemberPaths { get; }
		IEnumerable<ValidationError> Errors { get; }
		void AddError(ValidationError error);
	}

	public class ValidationContext : IValidationContext
	{
		public bool IsValid => !HasErrors;
		public bool HasErrors => Errors.Any();
		public string Scenario { get; }
		public IEnumerable<string> MemberPaths { get; }
		public IEnumerable<ValidationError> Errors => ValidationErrors;

		private ConcurrentQueue<ValidationError> ValidationErrors;

		public ValidationContext(
			string scenario,
			IEnumerable<string> memberPaths)
		{
			Scenario = string.IsNullOrWhiteSpace(scenario)
				? null
				: scenario;
			MemberPaths = memberPaths ?? Array.Empty<string>();
			ValidationErrors = new ConcurrentQueue<ValidationError>();
		}

		public void AddError(ValidationError error)
		{
			ValidationErrors.Enqueue(error);
		}
	}
}
