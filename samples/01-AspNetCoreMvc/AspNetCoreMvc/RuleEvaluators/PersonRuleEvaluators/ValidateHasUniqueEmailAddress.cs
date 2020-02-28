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
			string errorCode = null)
		{
			personValidator.AddValidatorFactory(sp => sp
				.GetRequiredService<PersonHasUniqueEmailAddressValidator>()
				.Configure(getErrorMessage: getErrorMessage, errorCode: errorCode));
		}
	}

	[Injectable(InjectableLifetime.Scoped)]
	public class PersonHasUniqueEmailAddressValidator : IValidator<Person>
	{
		public Type ClassToValidate => typeof(Person);

		private Func<string, string> GetErrorMessage;
		private string ErrorCode;
		private IPersonRepository PersonRepository;

		public PersonHasUniqueEmailAddressValidator(IPersonRepository personRepository)
		{
			PersonRepository = personRepository;
		}

		public PersonHasUniqueEmailAddressValidator Configure(
			Func<string, string> getErrorMessage = null,
			string errorCode = null)
		{
			GetErrorMessage = getErrorMessage ?? (_ => "Email address must be unique");
			ErrorCode = errorCode ?? "HasUniqueEmailAddress";
			return this;
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
					errorCode: ErrorCode,
					errorMessage: GetErrorMessage(person.EmailAddress),
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
