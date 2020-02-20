using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IValidationContext
	{
		bool HasErrors { get; }
		object AggregateRoot { get; }
		string Scenario { get; }
		IEnumerable<string> MemberPaths { get; }
		IEnumerable<ValidationError> Errors { get; }
		void AddError(ValidationError error);
	}

	public sealed class ValidationContext : IValidationContext
	{
		public bool HasErrors => Errors.Any();
		public object AggregateRoot { get; }
		public string Scenario { get; }
		public IEnumerable<string> MemberPaths { get; }
		public IEnumerable<ValidationError> Errors => ValidationErrors;

		private ConcurrentQueue<ValidationError> ValidationErrors;

		public ValidationContext(
			object aggregateRoot,
			string scenario,
			IEnumerable<string> memberPaths)
		{
			if (aggregateRoot == null)
				throw new ArgumentNullException(nameof(aggregateRoot));

			AggregateRoot = aggregateRoot;
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
