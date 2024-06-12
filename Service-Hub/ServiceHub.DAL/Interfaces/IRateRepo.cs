using ServiceHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Interfaces
{
    public interface IRateRepo
    {
        Task<IEnumerable<Rate>> GetAllRatingByWorkerId(int workerId);
        Task AddRate(int workerId, int userId, int value , string review);


    }
}
