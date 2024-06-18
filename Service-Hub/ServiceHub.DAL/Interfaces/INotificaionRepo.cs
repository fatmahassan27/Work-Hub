using ServiceHub.DAL.Entities;
using ServiceHub.DAL.GenericRepository;

namespace ServiceHub.DAL.Interfaces
{
    public interface INotificaionRepo : IGenericRepo<Notification>
    {
        Task<IEnumerable<Notification>> GetAllNotificationsByOwnerId(int ownerId);

    }
}
