using AutoMapper;
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
    public class ChatMessageService : IChatMessageService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ChatMessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task CreateMessage(ChatDTO obj)
        {
            var data =  mapper.Map<ChatMessage>(obj);
            await unitOfWork.ChatMessageRepo.CreateAsync(data);
            await unitOfWork.saveAsync();
        }

        public async Task<IEnumerable<ChatDTO>> GetAllMessages(int senderId, int receiverId)
        {
            var messages= await unitOfWork.ChatMessageRepo.GetAllMessages(senderId,receiverId);
            var messageDTOs = mapper.Map<IEnumerable<ChatDTO>>(messages);
            return messageDTOs;
        }
    }
}
