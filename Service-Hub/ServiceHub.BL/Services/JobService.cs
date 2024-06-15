using AutoMapper;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.UnitOfWork;

namespace ServiceHub.BL.Services
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public JobService(IUnitOfWork u,IMapper mapper)
        {
            this.unit = u;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<JobDTO>> GetAllJobs()
        {
            var jobs = await unit.JobRepo.GetAllAsync();
            var jobDTOs =  mapper.Map<IEnumerable<JobDTO>>(jobs);
            return jobDTOs;
        }

        public async Task<JobDTO> GetJobById(int id)
        {
            var job = await unit.JobRepo.GetByIdAsync(id);
            var jobDTO = mapper.Map<JobDTO>(job);
            return jobDTO;
        }

        public async Task UpdateJob(JobDTO jobDTO)
        {
            var job = mapper.Map<Job>(jobDTO);
           await unit.JobRepo.UpdateAsync(job.Id, job);
            await unit.saveAsync();
        }
    }
}
