//using Microsoft.AspNetCore.SignalR;
//using ServiceHub.DAL.DataBase;
//using ServiceHub.DAL.Entity;
//namespace ServiceHub.PL.Hubs
//{
//    public class NotificationsHub : Hub
//    {
//        private readonly ApplicationDbContext db;

//        public NotificationsHub(ApplicationDbContext _db)
//        {
//            db = _db;
//        }
//        public override Task OnConnectedAsync()
//        {
//            return base.OnConnectedAsync();
//        }
//        public override Task OnDisconnectedAsync(Exception? exception)
//        {
//            return base.OnDisconnectedAsync(exception);
//        }

//        public void SendOrderCreatedNotification(string userId, string workerId)
//        {
//            //to user
//            var userNotification = db.Notifications.Add(new Notification()
//            {
//                UserId = userId,
//                Title = "Success",
//                Content = "Order Created Successfully, wait for worker confirmation.",
//                CreatedDate = DateTime.Now,
//            });
//            //to worker
//            var workerNotification = db.Notifications.Add(new Notification()
//            {
//                WorkerId = workerId,
//                Title = "New Work Order",
//                Content = $"Congratulations! /n user: {db.Users.Find(userId).UserName} is trying to hire you!",
//                CreatedDate = DateTime.Now,
//            });

//            //db.UserConnections.Add(new UserConnection()
//            //{
//            //    UserId = userId,
//            //    ConnectionId = Context.ConnectionId
//            //});

//            Clients.Caller.SendAsync("NewNotification", userNotification);  // caller is the user
//                                                                            // Clients.Client(workerid).SendAsync("NewNotification", workerNotification);
//        }


//        public void SendOrderAcceptedNotification(string userId, string workerId)
//        {
//            //to user
//            var userNotification = db.Notifications.Add(new Notification()
//            {
//                UserId = userId,
//                Title = "Order Accepted",
//                Content = $"The Worker {db.Users.Find(workerId).UserName} accepted your offer. \n please rate the worker When the job is done.",
//                CreatedDate = DateTime.Now,
//            });
//            //to worker
//            var workerNotification = db.Notifications.Add(new Notification()
//            {
//                WorkerId = workerId,
//                Title = "Order Started",
//                Content = $"You can start your job now. \n The user {db.Users.Find(userId).UserName} is going to rate you when you are done.",
//                CreatedDate = DateTime.Now,
//            });

//            Clients.User(userId).SendAsync("NewNotification", userNotification);
//            Clients.Caller.SendAsync("NewNotification", workerNotification);    //caller is the worker
//        }

//        public void SendOrderDoneNotification(string userId, string workerId)
//        {
//            //to user
//            var userNotification = db.Notifications.Add(new Notification()
//            {
//               // UserId = userId,
//                Title = "Order Done",
//                Content = $"The Worker {db.Users.Find(workerId).UserName} has finished the required job. please give him a rating",
//                CreatedDate = DateTime.Now,
//            });
//            //to worker
//            var workerNotification = db.Notifications.Add(new Notification()
//            {
//               // WorkerId = workerId,
//                Title = "Order Done",
//                Content = $"You have successfully finished your work for the user {db.Users.Find(userId).UserName} . \n Good Luck in next Orders",
//                CreatedDate = DateTime.Now,
//            });

//            Clients.Caller.SendAsync("NewNotification", userNotification);      //caller is the user
//            Clients.User(workerId).SendAsync("NewNotification", workerNotification);
//        }
//        public void SendOrderCancelledNotification(string userId, string workerId)
//        {
//            //to user
//            var userNotification = db.Notifications.Add(new Notification()
//            {
//                //UserId = userId,
//                Title = "Order Cancelled",
//                Content = $"The order with worker {db.Users.Find(workerId).UserName} is cancelled.",
//                CreatedDate = DateTime.Now,
//            });
//            //to worker
//            var workerNotification = db.Notifications.Add(new Notification()
//            {
//                //WorkerId = workerId,
//                Title = "Order Cancelled",
//                Content = $"The order with user {db.Users.Find(userId).UserName} is cancelled.",
//                CreatedDate = DateTime.Now,
//            });

//            Clients.Caller.SendAsync("NewNotification", userNotification);      //caller is the user
//            Clients.User(workerId).SendAsync("NewNotification", workerNotification);
//        }
//    }
//}
