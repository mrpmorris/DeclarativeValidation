using System.Threading.Tasks;

namespace AspNetCoreMvc.Services
{
	public class PersonRepository : IPersonRepository
	{
		public async Task<int> GetIdByEmailAddress(string emailAddress)
		{
			await Task.Delay(1000);
			return 42;
		}
	}
}
