using ServiceHub.DAL.Entities;

namespace ServiceHub.DAL.Interfaces
{
    public interface IRateRepo
    {
        Task<IEnumerable<Rate>> GetAllRatingByWorkerId(int workerId);
        Task AddRate(Rate rate);


    }
}
