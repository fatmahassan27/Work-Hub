using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.UnitOfWork;
using ServiceHub.BL.DTOs;
using AutoMapper;

namespace ServiceHub.BL.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public CityService(IUnitOfWork u,IMapper mapper)
        {
            this.unit = u;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CityDTO>> GetAllCity()
        {
            var cities = await unit.CityRepo.GetAllAsync();

            var cityDTOs = mapper.Map<IEnumerable<CityDTO>>(cities);

            return cityDTOs;
        }
    }
}
