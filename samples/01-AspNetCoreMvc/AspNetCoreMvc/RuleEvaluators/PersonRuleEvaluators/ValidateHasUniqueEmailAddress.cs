using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvc.Models;
using AspNetCoreMvc.Services;
using Microsoft.Extensions.DependencyInjection;
using PeterLeslieMorris.DeclarativeValidation;
using PeterLeslieMorris.DeclarativeValidation.Definitions;
using PeterLeslieMorris.DeclarativeValidation.DependencyInjection;

namespace AspNetCoreMvc.RuleEvaluators.PersonRuleEvaluators
{
	public static class ValidateHasUniqueEmailAddressExtension
	{
		public static void HasUniqueEmailAddress(
			this ClassValidator<Person> personValidator,
			Func<string, string> getErrorMessage = null,
			string errorCode = nameof(HasUniqueEmailAddress))
		{
			personValidator.AddValidatorFactory(sp => sp.GetRequiredService<PersonHasUniqueEmailAddressValidator>());
		}
	}

	[Injectable(InjectableScope.Scoped)]
	public class PersonHasUniqueEmailAddressValidator : IValidator<Person>
	{
		public Type ClassToValidate => typeof(Person);

		private IPersonRepository PersonRepository;

		public PersonHasUniqueEmailAddressValidator(IPersonRepository personRepository)
		{
			PersonRepository = personRepository;
		}

		public Task<bool> ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			string[] memberPathSoFar,
			Person person)
		{
			int count = PersonRepository.Query.Count(x => x != person && x.EmailAddress == person.EmailAddress);
			if (count > 0)
			{
				context.AddError(new ValidationError(
					errorCode: nameof(ValidateHasUniqueEmailAddressExtension.HasUniqueEmailAddress),
					errorMessage: "Email address must be unique",
					memberName: nameof(Person.EmailAddress),
					memberPath: String.Join('.', memberPathSoFar.Append(nameof(Person.EmailAddress))),
					getMemberIdentifier: () => new MemberIdentifier(person, nameof(Person.EmailAddress))));
			}
			return Task.FromResult(count == 0);
		}

		public Task<bool> ValidateAsync(
			IServiceProvider serviceProvider,
			IValidationContext context,
			string[] memberPathSoFar,
			object obj)
			=> ValidateAsync(
					serviceProvider: serviceProvider,
					context: context,
					memberPathSoFar: memberPathSoFar,
					obj: (Person)obj);
	}
}
