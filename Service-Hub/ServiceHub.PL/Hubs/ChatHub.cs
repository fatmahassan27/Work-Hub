using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Helper;
using System.Collections.Concurrent;

namespace ServiceHub.PL.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatMessageService chatMessageService;
        private readonly IUserConnectionService userConnectionService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<ChatMessage> _logger;

        public ChatHub(IChatMessageService chatMessageService, IUserConnectionService userConnectionService, UserManager<ApplicationUser> userManager, ILogger<ChatMessage> _logger)
        {
            this.chatMessageService = chatMessageService; 
            this.userConnectionService= userConnectionService;
            this.userManager = userManager; 
            this._logger = _logger;
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("Client Connected: " + Context.ConnectionId);

            //string id = db.UserConnections.id;
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine("Client DisConnected: " + Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
        public async Task sendmessage(ChatDTO chatDto)//path
        {
            //save db
           await chatMessageService.CreateMessage(chatDto);

            //ChatMessage _message = new ChatMessage()
            //{
            //    Message = message,
            //    SenderId = senderId,
            //    ReceiverId = reciverId

            //};
            //db.ChatMessage.Add(_message);
            //db.SaveChanges();
            Clients.User(chatDto.ReceiverId.ToString()).SendAsync("newmessage", chatDto.Message , chatDto.SenderId);

        }

        //public async Task SendMessageToUser(string userId, string message)
        //{
        //    if (UserConnections.TryGetValue(userId, out string connectionId))
        //    {
        //        string sender = Context.User.Identity.Name;
        //        await Clients.Client(connectionId).SendAsync("ReceiveMessage", sender, message);
        //    }
        //}
    }
}
