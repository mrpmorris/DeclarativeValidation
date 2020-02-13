using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation.RuleFactories
{
	public class ClassValidator : IValidator
	{
		private readonly IValidator[] Validators;

		public ClassValidator(IEnumerable<IValidator> validators)
		{
			Validators = (validators ?? Array.Empty<IValidator>()).ToArray();
		}

		public Task ValidateAsync(IServiceProvider serviceProvider, object value) =>
			Task.WhenAll(Validators.Select(x => x.ValidateAsync(serviceProvider, value)));
	}
}
