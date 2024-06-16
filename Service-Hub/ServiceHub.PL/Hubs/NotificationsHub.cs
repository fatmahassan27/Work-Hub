using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.Helper;
namespace ServiceHub.PL.Hubs
{
    public class NotificationsHub : Hub
    {
        private readonly INotificationService notificationService;
        private readonly UserManager<ApplicationUser> userManager;

        public NotificationsHub(INotificationService notificationService,UserManager<ApplicationUser> userManager)
        {
            this.notificationService = notificationService;
            this.userManager = userManager;
        }

        public override Task OnConnectedAsync()
        {
            //var ConnectedUser = Context.User.Identity.;
            //notificationService.GetAllNotificationsByOwnerId()
            //add to userConnection table connection => userId
            //Clients.User()
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            //delete from userconnection tabl
            //Clients.User().SendAsync();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendOrderCreatedNotification(int userId, int workerId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            var worker = await userManager.FindByIdAsync(workerId.ToString());
            
            var userNotificationDTO = new NotificationDTO()
            {
                IsRead = false,
                OwnerId = userId,
                Title = "Success",
                Content = "Order Created Successfully, wait for worker confirmation.",
                CreatedDate = DateTime.Now,
            };
            var workerNotificationDTO = new NotificationDTO()
            {
                IsRead = false,
                OwnerId = workerId,
                Title = "New Work Order",
                Content = $"Congratulations! /n user: {user?.UserName} is trying to hire you!",
                CreatedDate = DateTime.Now,
            };

            await notificationService.CreateAsync(userNotificationDTO);
            await notificationService.CreateAsync(workerNotificationDTO);
            /*db.UserConnections.Add(new UserConnection()
            {
                UserId = userId,
                ConnectionId = Context.ConnectionId
            });*/

            await Clients.User(user.Id.ToString()).SendAsync("NewNotification", userNotificationDTO);
            await Clients.User(worker.Id.ToString()).SendAsync("NewNotification", workerNotificationDTO);
            // caller is the user
        }
        public async void SendOrderAcceptedNotification(int userId, int workerId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            var worker = await userManager.FindByIdAsync(workerId.ToString());

            //to user

            var userNotificationDTO = new NotificationDTO()
            {
                IsRead = false,
                OwnerId = userId,
                Title = "Order Accepted",
                Content = $"The Worker {worker?.UserName} accepted your offer. \n please rate the worker When the job is done.",
                CreatedDate = DateTime.Now,
            };
            //to worker
            var workerNotificationDTO = new NotificationDTO()
            {
                IsRead = false,
                OwnerId = workerId,
                Title = "Order Started",
                Content = $"You can start your job now. \n The user {user?.UserName} is going to rate you when you are done.",
                CreatedDate = DateTime.Now,
            };

            await notificationService.CreateAsync(userNotificationDTO);
            await notificationService.CreateAsync(workerNotificationDTO);

            await Clients.User(user?.Id.ToString()).SendAsync("NewNotification", userNotificationDTO);
            await Clients.User(worker?.Id.ToString()).SendAsync("NewNotification", workerNotificationDTO);
        }
        public async void SendOrderDoneNotification(int userId, int workerId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            var worker = await userManager.FindByIdAsync(workerId.ToString());

            var userNotificationDTO = new NotificationDTO()
            {
                IsRead = false,
                OwnerId = userId ,
                Title = "Order Done",
                Content = $"The Worker {worker?.UserName} has finished the required job. please give him a rating",
                CreatedDate = DateTime.Now,
            };
            var workerNotificationDTO = new NotificationDTO()
            {
                IsRead = false,
                OwnerId = workerId ,
                Title = "Order Done",
                Content = $"You have successfully finished your work for the user {user?.UserName} . \n Good Luck in next Orders",
                CreatedDate = DateTime.Now,
            };
            
            await notificationService.CreateAsync(userNotificationDTO);
            await notificationService.CreateAsync(workerNotificationDTO);

            await Clients.User(user?.Id.ToString()).SendAsync("NewNotification", userNotificationDTO);
            await Clients.User(worker?.Id.ToString()).SendAsync("NewNotification", workerNotificationDTO);
        }
        public async void SendOrderCancelledNotification(int userId, int workerId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            var worker = await userManager.FindByIdAsync(workerId.ToString());

            var userNotificationDTO = new NotificationDTO()
            {
                IsRead = false,
                OwnerId = userId ,
                Title = "Order Cancelled",
                Content = $"The order with worker {worker?.UserName} is cancelled.",
                CreatedDate = DateTime.Now,
            };
            var workerNotificationDTO = new NotificationDTO()
            {
                //WorkerId = workerId,
                Title = "Order Cancelled",
                Content = $"The order with user {user?.UserName} is cancelled.",
                CreatedDate = DateTime.Now,
            };

            await notificationService.CreateAsync(userNotificationDTO);
            await notificationService.CreateAsync(workerNotificationDTO);

            await Clients.User(user?.Id.ToString()).SendAsync("NewNotification", userNotificationDTO);
            await Clients.User(worker?.Id.ToString()).SendAsync("NewNotification", workerNotificationDTO);
        }
    }
}
