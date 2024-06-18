using AutoMapper;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.UnitOfWork;

namespace ServiceHub.BL.Services
{
    public class RateService : IRateService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RateService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task AddRate(RateDTO rateDTO)
        {
            var rate = mapper.Map<Rate>(rateDTO);
            await unitOfWork.RateRepo.AddRate(rate);
            await unitOfWork.saveAsync();
        }

        public async Task<IEnumerable<RateDTO>> GetAllRatingsByWorkerId(int workerId)
        {
           var ratings = await unitOfWork.RateRepo.GetAllRatingsByWorkerId(workerId);
            var ratingsDTO = mapper.Map<IEnumerable<RateDTO>>(ratings);
            return  ratingsDTO;
        }

        public async Task<double> getAverageWorkerRating(int workerId)
        {
            return await unitOfWork.RateRepo.getAverageWorkerRating(workerId);
        }

    }
}
