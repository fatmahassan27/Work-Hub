using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTOs;

namespace ServiceHub.BL.Interfaces
{
	public interface IDistrictService
	{
        Task<IEnumerable<DistrictDTO>> GetAll();
        Task<IEnumerable<DistrictDTO>> GetAllDistrictsByCityId(int CityId);
		Task<DistrictDTO> getById(int id);


    }
}
