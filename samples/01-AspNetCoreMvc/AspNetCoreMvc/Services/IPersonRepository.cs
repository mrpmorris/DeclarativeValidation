using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvc.Models;

namespace AspNetCoreMvc.Services
{
	public interface IPersonRepository
	{
		Task<int> GetIdByEmailAddress(string emailAddress);
		IQueryable<Person> Query { get;  }
	}
}
