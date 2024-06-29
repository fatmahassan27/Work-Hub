using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Helper;
using ServiceHub.DAL.UnitOfWork;

namespace ServiceHub.BL.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IRateService rateService;
        private readonly IUnitOfWork unit;

        public WorkerService(UserManager<ApplicationUser> userManager,IMapper mapper,IRateService rateService,IUnitOfWork unit)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.rateService = rateService;
            this.unit = unit;
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
            var workessIds = workers.Select(x=>x.Id).ToList();
            var workersList =  await userManager.Users.Where(a => workessIds.Contains(a.Id)).Include("Job").Include("District").ToListAsync();
            var workersDTO = workersList.Select(x => new WorkerDTO
            {
                Id = x.Id,
                Email = x.Email,
                Rating = x.Rating,  
                UserName=x.UserName,
                job = new JobDTO() {
                    Id = x.Job.Id,
                    Name = x.Job.Name,
                    Price = x.Job.Price,
                },
                district =new DistrictDTO()
                {
                     id=x.District.Id,
                     Name= x.District.Name
                },
            }).ToList();

            //var workersDTO = mapper.Map<List<WorkerDTO>>(workersList);


            foreach (var workerDTO in workersDTO)
            {
                workerDTO.Rating = (int?)await rateService.getAverageWorkerRating(workerDTO.Id);


            }

            return workersDTO;
        }
       
        public async Task<IEnumerable<WorkerDTO>> GetAllWorkersByDistrictId(int districtId)
        {
            var workers = await userManager.Users.Where(a => a.DistrictId == districtId).Include("Job").Include("District").ToListAsync(); // && isDeleted == false 
        
            var workersDTO = mapper.Map<IEnumerable<WorkerDTO>>(workers);

            foreach (var workerDTO in workersDTO)
            {
                workerDTO.Rating = (int)await rateService.getAverageWorkerRating(workerDTO.Id);
            }

            return workersDTO;
        }

        public async Task<IEnumerable<WorkerDTO>> GetAllWorkersByJobId(int jobId)
        {
            var workers = await userManager.Users.Where(a => a.JobId == jobId).Include("Job").Include("District").ToListAsync();
            
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
            var workes= userManager.Users.Where(u=>u.Id==id).Include("Job").Include("District").FirstOrDefault();
            var workerDTO = mapper.Map<WorkerDTO>(workes);

            workerDTO.Rating = (int)await rateService.getAverageWorkerRating(workerDTO.Id);

            return workerDTO;
        }
    }
}
