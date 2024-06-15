
using ServiceHub.BL.DTOs;

namespace ServiceHub.BL.Interfaces
{
    public interface IWorkerService
    {
        Task<IEnumerable<WorkerDTO>> GetAllWorkers();
        Task<WorkerDTO> GetWorkerById(int id);
        Task DeleteWorker(int id);
        Task<IEnumerable<WorkerDTO>> GetAllWorkersByJobId(int jobId);
        Task<IEnumerable<WorkerDTO>> GetAllWorkersByDistrictId(int districtId);

    }
}
