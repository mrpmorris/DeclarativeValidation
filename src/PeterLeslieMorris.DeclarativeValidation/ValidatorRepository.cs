using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IValidatorRepository
	{
		IEnumerable<IClassValidator> GetClassValidators(Type type);
		IEnumerable<IClassValidator> GetClassValidators<TClass>();
		void AddClassValidator(IClassValidator classValidator);
		void AddClassValidators(IEnumerable<IClassValidator> classValidators);
	}
	public sealed class ValidatorRepository : IValidatorRepository
	{
		private ConcurrentDictionary<Type, List<IClassValidator>> ClassValidators;

		public ValidatorRepository()
		{
			ClassValidators = new ConcurrentDictionary<Type, List<IClassValidator>>();
		}

		public void AddClassValidator(IClassValidator classValidator)
		{
			if (classValidator == null)
				throw new ArgumentNullException(nameof(classValidator));

			ClassValidators.AddOrUpdate(
				key: classValidator.GetClassType(),
				addValueFactory: _ => new List<IClassValidator> { classValidator },
				updateValueFactory: (_, validators) =>
				{
					validators.Add(classValidator);
					return validators;
				});
		}

		public void AddClassValidators(IEnumerable<IClassValidator> classValidators)
		{
			foreach (var classValidator in classValidators)
				AddClassValidator(classValidator);
		}

		public IEnumerable<IClassValidator> GetClassValidators(Type type) =>
			ClassValidators.TryGetValue(type, out List<IClassValidator> validators)
			? validators
			: Enumerable.Empty<IClassValidator>();

		public IEnumerable<IClassValidator> GetClassValidators<TClass>() =>
			GetClassValidators(typeof(TClass));
	}
}
