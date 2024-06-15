using ServiceHub.BL.DTOs;
namespace ServiceHub.BL.Interfaces
{
    public interface IRateService
    {
        Task AddRate(RateDTO rateDTO);
        Task<IEnumerable<RateDTO>> GetAllRatingsByWorkerId(int workerId);
        Task<double> getAverageWorkerRating(int workerId);

    }
}
