
using ServiceHub.DAL.Entities;
using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.UnitOfWork;

namespace ServiceHub.BL.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork unit;

        public CityService(IUnitOfWork u)
        {
            this.unit = u;
        }
        public Task<IEnumerable<City>> GetAllCity()
        {
            return unit.CityRepo.GetAllAsync();
        }
    }
}
