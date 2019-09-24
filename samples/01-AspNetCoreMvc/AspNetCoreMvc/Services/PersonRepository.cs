using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvc.Models;

namespace AspNetCoreMvc.Services
{
	public class PersonRepository : IPersonRepository
	{
		public async Task<int> GetIdByEmailAddress(string emailAddress)
		{
			await Task.Delay(1000);
			return 42;
		}

		public IQueryable<Person> Query =>
			new List<Person>
			{
				new Person { EmailAddress = "me@home.com" },
				new Person { EmailAddress = "you@atwork.com" }
			}.AsQueryable();
	}
}
