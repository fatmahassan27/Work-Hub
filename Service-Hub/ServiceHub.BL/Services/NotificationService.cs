﻿using AutoMapper;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await unitOfWork.NotificaionRepo.CreateAsync(notification);
            await unitOfWork.saveAsync();
        }

        public async Task DeleteAsync(int notificationId)
        {
            await unitOfWork.NotificaionRepo.DeleteAsync(notificationId);
        }

        public async Task<IEnumerable<NotificationDTO>> GetAllNotificationsByOwnerId(int ownerId)
        {
            var notifications = await unitOfWork.NotificaionRepo.GetAllNotificationsByOwnerId(ownerId);
            var notificationsDTO = mapper.Map<IEnumerable<NotificationDTO>>(notifications);
            return notificationsDTO;

        }

        public async Task<NotificationDTO> GetByIdAsync(int id)
        {
            var notification = await unitOfWork.NotificaionRepo.GetByIdAsync(id);
            var notificationDTO = mapper.Map<NotificationDTO>(notification);
            return notificationDTO;
        }

        public async Task UpdateAsync(int noficationid, NotificationDTO notificationDTO)
        {
            var notification = mapper.Map<Notification>(notificationDTO);
            await unitOfWork.NotificaionRepo.UpdateAsync(noficationid, notification);
            await unitOfWork.saveAsync();
        }
    }
}
