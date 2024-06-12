using ServiceHub.DAL.Entities;

namespace ServiceHub.DAL.Interfaces
{
    public interface IRateRepo
    {
        IEnumerable<Rate> GetAllRatingsByWorkerId(int workerId);
        Task AddRate(Rate rate);




    }
}
