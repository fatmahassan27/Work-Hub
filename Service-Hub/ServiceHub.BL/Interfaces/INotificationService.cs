using ServiceHub.BL.DTOs;

namespace ServiceHub.BL.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDTO>> GetAllNotificationsByOwnerId(int ownerId);
        Task<NotificationDTO> GetByIdAsync(int id);
        Task CreateAsync(NotificationDTO notificationDTO);
        Task UpdateAsync(int noficationid, NotificationDTO notificationDTO);
        Task DeleteAsync(int notificationId);
    }
}
