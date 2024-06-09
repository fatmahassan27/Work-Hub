using ServiceHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Interfaces
{
    public interface IJobService
    {
        Task<IEnumerable<Job>> GetAllJobs();
        Task<Job> GetJobById(int id);
        Task UpdateJob(Job job);

    }
}
