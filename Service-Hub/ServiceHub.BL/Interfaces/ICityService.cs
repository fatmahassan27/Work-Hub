using ServiceHub.BL.DTOs;

namespace ServiceHub.BL.Interfaces
{
	public interface ICityService
	{
		Task<IEnumerable<CityDTO>> GetAllCity();
	}
}
