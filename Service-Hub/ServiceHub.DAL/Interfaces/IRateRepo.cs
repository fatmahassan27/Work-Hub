using ServiceHub.DAL.Entities;

namespace ServiceHub.DAL.Interfaces
{
    public interface IRateRepo
    {
        Task<IEnumerable<Rate>> GetAllRatingsByWorkerId(int workerId);
        Task AddRate(Rate rate);


    }
}
