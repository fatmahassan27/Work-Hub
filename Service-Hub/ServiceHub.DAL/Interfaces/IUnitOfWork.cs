
using ServiceHub.DAL.Entities;

namespace ServiceHub.DAL.Interfaces
{
    public interface IUnitOfWork 
    {
        IJobRepo JobRepo { get; }

        Task<int> saveAsync();
    }
}
