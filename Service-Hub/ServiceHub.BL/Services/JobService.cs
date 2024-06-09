
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Interfaces;

namespace ServiceHub.BL.Services
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork unit;

        public JobService(IUnitOfWork u)
        {
            this.unit = u;
        }
        public async Task<IEnumerable<Job>> GetAllJobs()
        {
            return await unit.JobRepo.GetAllAsync();
        }

        public async Task<Job> GetJobById(int id)
        {
            return await unit.JobRepo.GetByIdAsync(id);
        }

        public async Task UpdateJob(Job job)
        {
            await unit.JobRepo.UpdateAsync(job.Id , job);
            unit.saveAsync();
        }
    }
}
