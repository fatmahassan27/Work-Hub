using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Helper;
using System.Security.Claims;

namespace ServiceHub.PL.Hubs
{
    public class NotificationsHub : Hub
    {
        private readonly INotificationService notificationService;
        private readonly IUserConnectionService userConnectionService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<NotificationsHub> _logger;

        public NotificationsHub(
            INotificationService notificationService,
            IUserConnectionService userConnectionService,
            UserManager<ApplicationUser> userManager,
            ILogger<NotificationsHub> logger)
        {
            this.notificationService = notificationService;
            this.userConnectionService = userConnectionService;
            this.userManager = userManager;
            this._logger = logger;
        }

        //public async override Task OnConnectedAsync()
        //{

        //    Console.WriteLine("Client connected: " + Context.ConnectionId);

        //    Retrieve the current user ID from the identity system
        //    add to db
        //    var userConnection = new UserConnection() { UserId = userId, ConnectionId = Context.ConnectionId };
        //    await userConnectionService.CreateAsync(userConnection);

        //    await base.OnConnectedAsync();
        //}
        public async override Task OnConnectedAsync()
        {
            try
            {
                Console.WriteLine("Client connected: " + Context.ConnectionId);
                var userId = Context.User?.FindFirst("id")?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found for connection ID: {ConnectionId}", Context.ConnectionId);
                    return;
                }

                var userConnection = new UserConnection
                {
                    UserId = int.Parse(userId), // Ensure this matches your UserId type
                    ConnectionId = Context.ConnectionId
                };

                await userConnectionService.CreateAsync(userConnection);

                _logger.LogInformation("Client connected: {ConnectionId}, UserId: {UserId}", Context.ConnectionId, userId);

                await base.OnConnectedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while connecting: {ConnectionId}", Context.ConnectionId);
                throw; // Re-throw the exception to let SignalR handle the disconnection
            }
        }



        //public async override Task OnDisconnectedAsync(Exception? exception)
        //    {
        //        Console.WriteLine("Client disconnected: " + Context.ConnectionId);
        //        var userConn = await userConnectionService.GetRowByConnectionId(Context.ConnectionId);
        //        userConnectionService.RemoveAsync(userConn);
        //        if (exception != null)
        //        {
        //            Console.WriteLine("Disconnection error: " + exception.Message);
        //        }
        //         base.OnDisconnectedAsync(exception);
        //    }
        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                Console.WriteLine("Client disconnected: " + Context.ConnectionId);

                var userConn = await userConnectionService.GetRowByConnectionId(Context.ConnectionId);
                if (userConn != null)
                {
                    await userConnectionService.RemoveAsync(userConn);
                    Console.WriteLine("Removed connection: " + Context.ConnectionId);
                }

                if (exception != null)
                {
                    Console.WriteLine("Disconnection error: " + exception.Message);
                }

                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during disconnection: " + ex.Message);
                throw; // Re-throw the exception to let SignalR handle any further cleanup
            }
        }


        public async Task SendOrderCreatedNotification(int userId, int workerId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId.ToString());
                var worker = await userManager.FindByIdAsync(workerId.ToString());

                if (user == null || worker == null)
                {
                    _logger.LogError($"User or worker not found: userId={userId}, workerId={workerId}");
                    throw new ArgumentException("User or worker not found");
                }

                var userConnection = new UserConnection() { UserId = userId, ConnectionId = Context.ConnectionId };
                await userConnectionService.CreateAsync(userConnection);
                

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

                //await Clients.All.SendAsync("NewNotification", userNotificationDTO);
                //await Clients.Caller.SendAsync("NewNotification", userNotificationDTO);
                await Clients.User(user.Id.ToString()).SendAsync("NewNotification", userNotificationDTO);
                await Clients.User(worker.Id.ToString()).SendAsync("NewNotification", workerNotificationDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SendOrderCreatedNotification");
                throw;
            }
        }

        public async Task SendOrderAcceptedNotification(int userId, int workerId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId.ToString());
                var worker = await userManager.FindByIdAsync(workerId.ToString());

                if (user == null || worker == null)
                {
                    _logger.LogError($"User or worker not found: userId={userId}, workerId={workerId}");
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

                await Clients.User(user.Id.ToString()).SendAsync("NewNotification", userNotificationDTO);
                await Clients.User(worker.Id.ToString()).SendAsync("NewNotification", workerNotificationDTO);
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error in SendOrderAcceptedNotification");
                throw;
            }
        }

        public async Task SendOrderDoneNotification(int userId, int workerId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId.ToString());
                var worker = await userManager.FindByIdAsync(workerId.ToString());

                if (user == null || worker == null)
                {
                    _logger.LogError($"User or worker not found: userId={userId}, workerId={workerId}");
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

                await Clients.User(user.Id.ToString()).SendAsync("NewNotification", userNotificationDTO);
                await Clients.User(worker.Id.ToString()).SendAsync("NewNotification", workerNotificationDTO);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error in SendOrderDoneNotification");
                throw;
            }
           
        }

        public async Task SendOrderCancelledNotification(int userId, int workerId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId.ToString());
                var worker = await userManager.FindByIdAsync(workerId.ToString());

                if (user == null || worker == null)
                {
                    _logger.LogError($"User or worker not found: userId={userId}, workerId={workerId}");
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

                await Clients.User(user.Id.ToString()).SendAsync("NewNotification", userNotificationDTO);
                await Clients.User(worker.Id.ToString()).SendAsync("NewNotification", workerNotificationDTO);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error in SendOrderCancelledNotification");
                throw;
            }
            
        }
    }
}
