using ServiceHub.DAL.Entities;

namespace ServiceHub.BL.Interfaces
{
	public interface ICityService
	{
		Task<IEnumerable<City>> GetAllCity();
	}
}
