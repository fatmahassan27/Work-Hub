using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.UnitOfWork;
using ServiceHub.BL.DTOs;
using AutoMapper;
using ServiceHub.DAL.Entities;

namespace ServiceHub.BL.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public DistrictService(IUnitOfWork u,IMapper mapper)
        {
            this.unit = u;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<DistrictDTO>> GetAll()
        {
            var districts = await unit.DistrictRepo.GetAllAsync();

            var districtsDTO = mapper.Map<IEnumerable<DistrictDTO>>(districts);

            return districtsDTO;
        }

        public async Task<IEnumerable<DistrictDTO>> GetAllDistrictsByCityId(int CityId)
        {
            var districts = await unit.DistrictRepo.GetAllDistrictsByCityId(CityId);

            var districtsDTO = mapper.Map<IEnumerable<DistrictDTO>>(districts);

            return districtsDTO;
        }

        public async Task<DistrictDTO> getById(int id)
        {
            var district = await unit.DistrictRepo.GetByIdAsync(id);
            var districtDTO = mapper.Map<DistrictDTO>(district);
            return districtDTO;

        }
    }
}
