using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public interface IClassValidatorRepository
	{
		public void AddValidator<TClass>(IValidator<TClass> validator);
		public IEnumerable<IValidator> GetValidators(Type classToValidate);
		public IEnumerable<IValidator<TClass>> GetValidators<TClass>();
	}

	public class ClassValidatorRepository : IClassValidatorRepository
	{
		private readonly System.Reflection.Assembly SystemAssembly;
		private readonly ConcurrentDictionary<Type, IValidator[]> ValidatorsByType;

		public ClassValidatorRepository()
		{
			SystemAssembly = typeof(object).Assembly;
			ValidatorsByType = new ConcurrentDictionary<Type, IValidator[]>();
		}

		internal void AddValidator(IValidator validator)
		{
			if (validator == null)
				throw new ArgumentNullException(nameof(validator));

			ValidatorsByType.AddOrUpdate(
				key: validator.ClassToValidate,
				addValueFactory: _ => (new List<IValidator> { validator }).ToArray(),
				updateValueFactory: (_, validators) => validators.Append(validator).ToArray());
		}

		public void AddValidator<TClass>(IValidator<TClass> validator) =>
			AddValidator((IValidator)validator);

		public IEnumerable<IValidator> GetValidators(Type aggregateRootType)
		{
			if (aggregateRootType == null)
				throw new ArgumentNullException(nameof(aggregateRootType));

			Type currentType = aggregateRootType;
			var result = new List<IValidator>();
			while (currentType != null)
			{
				if (currentType == null || currentType.Assembly == SystemAssembly)
					break;

				IValidator[] validators;
				if (ValidatorsByType.TryGetValue(currentType, out validators))
					result.AddRange(validators);

				currentType = currentType.BaseType;
			}
			return result;
		}

		public IEnumerable<IValidator<TAggregateRoot>> GetValidators<TAggregateRoot>() =>
			GetValidators(typeof(TAggregateRoot)).Cast<IValidator<TAggregateRoot>>();
	}
}
