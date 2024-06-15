using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.Helper;

namespace ServiceHub.BL.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IRateService rateService;

        public WorkerService(UserManager<ApplicationUser> userManager,IMapper mapper,IRateService rateService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.rateService = rateService;
        }

        public async Task DeleteWorker(int id)
        {
            var worker = await userManager.FindByIdAsync(id.ToString());
            if (worker != null)
            {
                if (await userManager.IsInRoleAsync(worker, "Worker"))
                {
                    worker.IsDeleted = true;
                    await userManager.UpdateAsync(worker);
                }                
            }
        }

        public async Task<IEnumerable<WorkerDTO>> GetAllWorkers()
        {
            var workers = await userManager.GetUsersInRoleAsync("Worker");
           
            var workersDTO = mapper.Map<IEnumerable<WorkerDTO>>(workers);

            foreach (var workerDTO in workersDTO)
            {
                workerDTO.Rating = (int)await rateService.getAverageWorkerRating(workerDTO.Id);
            }

            return workersDTO;
        }

        public async Task<IEnumerable<WorkerDTO>> GetAllWorkersByDistrictId(int districtId)
        {
            var workers = await userManager.Users.Where(a => a.DistrictId == districtId).ToListAsync(); // && isDeleted == false 
        
            var workersDTO = mapper.Map<IEnumerable<WorkerDTO>>(workers);

            foreach (var workerDTO in workersDTO)
            {
                workerDTO.Rating = (int)await rateService.getAverageWorkerRating(workerDTO.Id);
            }

            return workersDTO;
        }

        public async Task<IEnumerable<WorkerDTO>> GetAllWorkersByJobId(int jobId)
        {
            var workers = await userManager.Users.Where(a => a.JobId == jobId).ToListAsync();
            
            var workersDTO = mapper.Map<IEnumerable<WorkerDTO>>(workers);

            foreach (var workerDTO in workersDTO)
            {
                workerDTO.Rating = (int)await rateService.getAverageWorkerRating(workerDTO.Id);
            }

            return workersDTO;

        }

        public async Task<WorkerDTO> GetWorkerById(int id)
        {
            var worker = await userManager.FindByIdAsync(id.ToString()); 
            
            var workerDTO = mapper.Map<WorkerDTO>(worker);

            workerDTO.Rating = (int)await rateService.getAverageWorkerRating(workerDTO.Id);

            return workerDTO;
        }
    }
}
