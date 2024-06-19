using AutoMapper;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.UnitOfWork;

namespace ServiceHub.BL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public NotificationService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task CreateAsync(NotificationDTO notificationDTO)
        {
            var notification = mapper.Map<Notification>(notificationDTO);
            await unitOfWork.NotificationRepo.CreateAsync(notification);
            await unitOfWork.saveAsync();
        }

        public async Task DeleteAsync(int notificationId)
        {
            await unitOfWork.NotificationRepo.DeleteAsync(notificationId);
        }

        public async Task<IEnumerable<NotificationDTO>> GetAllNotificationsByOwnerId(int ownerId)
        {
            var notifications = await unitOfWork.NotificationRepo.GetAllNotificationsByOwnerId(ownerId);
            var notificationsDTO = mapper.Map<IEnumerable<NotificationDTO>>(notifications);
            return notificationsDTO;

        }

        public async Task<NotificationDTO> GetByIdAsync(int id)
        {
            var notification = await unitOfWork.NotificationRepo.GetByIdAsync(id);
            var notificationDTO = mapper.Map<NotificationDTO>(notification);
            return notificationDTO;
        }

        public async Task UpdateAsync(int noficationid, NotificationDTO notificationDTO)
        {
            var notification = mapper.Map<Notification>(notificationDTO);
            await unitOfWork.NotificationRepo.UpdateAsync(noficationid, notification);
            await unitOfWork.saveAsync();
        }

    }
}
