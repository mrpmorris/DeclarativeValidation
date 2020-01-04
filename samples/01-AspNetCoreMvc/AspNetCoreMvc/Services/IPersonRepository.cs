using System.Threading.Tasks;

namespace AspNetCoreMvc.Services
{
	public interface IPersonRepository
	{
		Task<int> GetIdByEmailAddress(string emailAddress);
	}
}
