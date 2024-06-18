using ServiceHub.BL.DTOs;

namespace ServiceHub.BL.Interfaces
{
	public interface IDistrictService
	{
		Task<IEnumerable<DistrictDTO>> GetAllDistrictsByCityId(int CityId);
	}
}
