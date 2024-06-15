
using ServiceHub.DAL.Entities;
using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.UnitOfWork;

namespace ServiceHub.BL.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly IUnitOfWork unit;

        public DistrictService(IUnitOfWork u)
        {
            this.unit = u;
        }
        public Task<IEnumerable<District>> GetAllByCityId(int CityId)
        {
            return unit.DistrictRepo.GetAllDistrictsByCityId(CityId);
        }
    }
}
