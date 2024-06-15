using ServiceHub.DAL.Entities;

namespace ServiceHub.BL.Interfaces
{
	public interface IDistrictService
	{
		Task<IEnumerable<District>> GetAllByCityId(int CityId);
	}
}
