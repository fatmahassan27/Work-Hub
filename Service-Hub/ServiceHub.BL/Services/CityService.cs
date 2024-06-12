using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
