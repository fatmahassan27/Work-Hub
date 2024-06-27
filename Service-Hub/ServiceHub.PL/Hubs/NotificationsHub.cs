using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Helper;
using System.Diagnostics;
using System.Security.Claims;

namespace ServiceHub.PL.Hubs
{
    [Authorize]
    public class NotificationsHub : Hub
    {
        private readonly INotificationService notificationService;
        private readonly IUserConnectionService userConnectionService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<NotificationsHub> logger;

        public NotificationsHub(
            INotificationService notificationService,
            IUserConnectionService userConnectionService,
            UserManager<ApplicationUser> userManager,
            ILogger<NotificationsHub> logger)
        {
            this.notificationService = notificationService;
            this.userConnectionService = userConnectionService;
            this.userManager = userManager;
            this.logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    if (string.IsNullOrEmpty(userId))
                    {
                        logger.LogWarning("User ID not found for connection ID: {ConnectionId}", Context.ConnectionId);
                        return;
                    }

                    var userConnection = new UserConnection
                    {
                        UserId = int.Parse(userId),
                        ConnectionId = Context.ConnectionId
                    };

                    await userConnectionService.CreateAsync(userConnection);

                    logger.LogInformation("Client connected: {ConnectionId}, UserId: {UserId}", Context.ConnectionId, userId);
                }
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
       
        public async Task sendordercreatednotification(int userId, int workerId)
        {//triggered by user
            try
            {
                await Console.Out.WriteLineAsync("hi");
                var user = await userManager.FindByIdAsync(userId.ToString());
                var worker = await userManager.FindByIdAsync(workerId.ToString());

                if (user == null || worker == null)
                {
                    logger.LogError($"User or worker not found: userId={userId}, workerId={workerId}");
                    throw new ArgumentException("User or worker not found");
                }

                var userNotificationDTO = new NotificationDTO
                {
                    IsRead = false,
                    OwnerId = userId,
                    Title = "Success",
                    Content = "Order Created Successfully, wait for worker confirmation.",
                    CreatedDate = DateTime.Now,
                };
                var workerNotificationDTO = new NotificationDTO
                {
                    IsRead = false,
                    OwnerId = workerId,
                    Title = "New Work Order",
                    Content = $"Congratulations! \n user: {user.UserName} is trying to hire you!",
                    CreatedDate = DateTime.Now,
                };

                await notificationService.CreateAsync(userNotificationDTO);
                await notificationService.CreateAsync(workerNotificationDTO);

                await Clients.Caller.SendAsync("NewNotification", userNotificationDTO);
                await SendNotificationToUserAsync(workerNotificationDTO, workerId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in SendOrderCreatedNotification");
                throw;
            }
        }
        public async Task SendOrderAcceptedNotification(int userId, int workerId)
        {//triggered by worker
            try
            {
                var user = await userManager.FindByIdAsync(userId.ToString());
                var worker = await userManager.FindByIdAsync(workerId.ToString());

                if (user == null || worker == null)
                {
                    logger.LogError($"User or worker not found: userId={userId}, workerId={workerId}");
                    throw new ArgumentException("User or worker not found");
                }

                var userNotificationDTO = new NotificationDTO
                {
                    IsRead = false,
                    OwnerId = userId,
                    Title = "Order Accepted",
                    Content = $"The Worker {worker.UserName} accepted your offer. \n please rate the worker when the job is done.",
                    CreatedDate = DateTime.Now,
                };
                var workerNotificationDTO = new NotificationDTO
                {
                    IsRead = false,
                    OwnerId = workerId,
                    Title = "Order Started",
                    Content = $"You can start your job now. \n The user {user.UserName} is going to rate you when you are done.",
                    CreatedDate = DateTime.Now,
                };

                await notificationService.CreateAsync(userNotificationDTO);
                await notificationService.CreateAsync(workerNotificationDTO);

                await SendNotificationToUserAsync(userNotificationDTO, userId);
                await Clients.Caller.SendAsync("NewNotification", workerNotificationDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in SendOrderAcceptedNotification");
                throw;
            }
        }
        public async Task SendOrderDoneNotification(int userId, int workerId)
        {//triggered by worker
            try
            {
                var user = await userManager.FindByIdAsync(userId.ToString());
                var worker = await userManager.FindByIdAsync(workerId.ToString());

                if (user == null || worker == null)
                {
                    logger.LogError($"User or worker not found: userId={userId}, workerId={workerId}");
                    throw new ArgumentException("User or worker not found");
                }

                var userNotificationDTO = new NotificationDTO
                {
                    IsRead = false,
                    OwnerId = userId,
                    Title = "Order Done",
                    Content = $"The Worker {worker.UserName} has finished the required job. Please give him a rating.",
                    CreatedDate = DateTime.Now,
                };

                var workerNotificationDTO = new NotificationDTO
                {
                    IsRead = false,
                    OwnerId = workerId,
                    Title = "Order Done",
                    Content = $"You have successfully finished your work for the user {user.UserName}. \n Good luck with your next orders.",
                    CreatedDate = DateTime.Now,
                };

                await notificationService.CreateAsync(userNotificationDTO);
                await notificationService.CreateAsync(workerNotificationDTO);

                var userConnectionIds = await userConnectionService.GetRowsByUserId(userId);
                var workerConnectionIds = await userConnectionService.GetRowsByUserId(workerId);

                if (userConnectionIds != null && userConnectionIds.Any())
                {
                    foreach (var row in userConnectionIds)
                    {
                        try
                        {
                            await Clients.Client(row.ConnectionId).SendAsync("NewNotification", userNotificationDTO);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, $"Failed to send notification to connection {row.ConnectionId}");
                        }
                    }
                }
                if (workerConnectionIds != null && workerConnectionIds.Any())
                {
                    foreach (var row in workerConnectionIds)
                    {
                        try
                        {
                            await Clients.Client(row.ConnectionId).SendAsync("NewNotification", workerNotificationDTO);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, $"Failed to send notification to connection {row.ConnectionId}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in SendOrderDoneNotification");
                throw;
            }

        }
        public async Task SendOrderCancelledNotification(int userId, int workerId)
        {//triggered by user or worker
            try
            {
                var user = await userManager.FindByIdAsync(userId.ToString());
                var worker = await userManager.FindByIdAsync(workerId.ToString());

                if (user == null || worker == null)
                {
                    logger.LogError($"User or worker not found: userId={userId}, workerId={workerId}");
                    throw new ArgumentException("User or worker not found");
                }

                var userNotificationDTO = new NotificationDTO
                {
                    IsRead = false,
                    OwnerId = userId,
                    Title = "Order Cancelled",
                    Content = $"The order with worker {worker.UserName} is cancelled.",
                    CreatedDate = DateTime.Now,
                };

                var workerNotificationDTO = new NotificationDTO
                {
                    IsRead = false,
                    OwnerId = workerId,
                    Title = "Order Cancelled",
                    Content = $"The order with user {user.UserName} is cancelled.",
                    CreatedDate = DateTime.Now,
                };

                await notificationService.CreateAsync(userNotificationDTO);
                await notificationService.CreateAsync(workerNotificationDTO);
               
                var userConnectionIds = await userConnectionService.GetRowsByUserId(userId);
                var workerConnectionIds = await userConnectionService.GetRowsByUserId(workerId);

                await SendNotificationToUserAsync(userNotificationDTO, userId);
                await SendNotificationToUserAsync(workerNotificationDTO, workerId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in SendOrderCancelledNotification");
                throw;
            }

        }

        private async Task SendNotificationToUserAsync(NotificationDTO notificationDTO, int userId)
        {
            var userConnectionIds = await userConnectionService.GetRowsByUserId(userId);
            if (userConnectionIds != null && userConnectionIds.Any())
            {
                foreach (var row in userConnectionIds)
                {
                    try
                    {
                        await Clients.Client(row.ConnectionId).SendAsync("NewNotification", notificationDTO);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"Failed to send notification to connection {row.ConnectionId}");
                        // Handle or log the error as needed
                    }
                }
            }
        }
    }
}
