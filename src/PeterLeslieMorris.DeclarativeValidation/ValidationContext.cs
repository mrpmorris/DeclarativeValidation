using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class ValidationContext
	{
		public object AggregateRoot { get; }
		public string Scenario { get; }
		public bool HasErrors => Errors.Any();
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
