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
using System.Security.Claims;

namespace ServiceHub.PL.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatMessageService chatMessageService;
        private readonly IUserConnectionService userConnectionService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<ChatMessage> logger;

        public ChatHub(IChatMessageService chatMessageService, IUserConnectionService userConnectionService, UserManager<ApplicationUser> userManager, ILogger<ChatMessage> logger)
        {
            this.chatMessageService = chatMessageService; 
            this.userConnectionService= userConnectionService;
            this.userManager = userManager; 
            this.logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    logger.LogWarning("User ID not found for connection ID: {ConnectionId}", Context.ConnectionId);
                    return ;
                }

                var userConnection = new UserConnection
                {
                    UserId = int.Parse(userId),
                    ConnectionId = Context.ConnectionId
                };

                await userConnectionService.CreateAsync(userConnection);

                logger.LogInformation("Client connected: {ConnectionId}, UserId: {UserId}", Context.ConnectionId, userId);

                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while connecting: {ConnectionId}", Context.ConnectionId);
                throw; // Re-throw the exception to let SignalR handle the disconnection
            }
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                logger.LogInformation("Client disconnected: {ConnectionId}", Context.ConnectionId);

                var userConn = await userConnectionService.GetRowByConnectionId(Context.ConnectionId);
                if (userConn != null)
                {
                    await userConnectionService.RemoveAsync(userConn);
                    logger.LogInformation("Removed connection: {ConnectionId}", Context.ConnectionId);
                }

                if (exception != null)
                {
                    logger.LogError("Disconnection error: {Error}", exception.Message);
                }

                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during disconnection: {ConnectionId}", Context.ConnectionId);
                throw; // Re-throw the exception to let SignalR handle any further cleanup
            }
        }
        public async Task sendmessage(int userId, int workerId)//path
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId.ToString());
                var worker = await userManager.FindByIdAsync(workerId.ToString());
                 if (user == null || worker == null)
                 {
                    logger.LogError($"User or worker not found: userId={userId}, workerId={workerId}");
                    throw new ArgumentException("User or worker not found");
                 }
                var chatMessage = new ChatDTO
                {

                    SenderId = userId,
                    ReceiverId = workerId,
                    Message = "HELLO WORLD",
                    IsSeen = false,
                    createdDate = DateTime.Now
                };
                await chatMessageService.CreateMessage(chatMessage);
                await Clients.Caller.SendAsync("SendMessage", chatMessage);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in SendOrderCreatedNotification");
                throw;
            }
          

        }

   
    }
}
