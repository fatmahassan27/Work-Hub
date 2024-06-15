using ServiceHub.BL.DTOs;

namespace ServiceHub.BL.Interfaces;

public interface IJobService
{
    Task<IEnumerable<JobDTO>> GetAllJobs();
    Task<JobDTO> GetJobById(int id);
    Task UpdateJob(JobDTO jobDTO);

}
