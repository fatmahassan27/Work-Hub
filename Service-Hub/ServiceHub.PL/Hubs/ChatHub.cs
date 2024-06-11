//using AutoMapper;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.SignalR;
//using ServiceHub.DAL.DataBase;
//using ServiceHub.DAL.Entities;
//using ServiceHub.DAL.Helper;

//namespace ServiceHub.PL.Hubs
//{
//    public class ChatHub : Hub
//    {
//        private readonly ApplicationDbContext db;
//        private readonly UserManager<ApplicationUser> userManager;
//        private readonly IMapper mapper;

//        public ChatHub(ApplicationDbContext db, UserManager<ApplicationUser> userManager, IMapper mapper)
//        {
//            this.db = db;
//            this.userManager = userManager;
//            this.mapper = mapper;
//        }

//        public override Task OnConnectedAsync()
//        {
//            string userId = Context.UserIdentifier;
//            UserConnection[userId] = Context.ConnectionId;
//            return base.OnConnectedAsync();
//        }
//        public override Task OnDisconnectedAsync(Exception exception)
//        {
//            string userId = Context.UserIdentifier;
//            UserConnection.TryRemove(userId, out _);
//            return base.OnDisconnectedAsync(exception);
//        }
//        public async Task sendmessage(string messId, string message, string userId, string workerId, string user)//path
//        {
//            var user1 = db.user.Where(a => a.Id == userId).FirstOrDefault();
//            var worker = db.ApplicationUsers.Where(a => a.Id == workerId).FirstOrDefault();

//            var messages =
//            ChatMessage _message = new ChatMessage()
//            {
//                Message = message,
//                Id = int.Parse(messId),
//                WorkerId = workerId,
//                UserId = userId

//            };
//            db.ChatMessage.Add(_message);
//            db.SaveChanges();
//            Clients.All.SendAsync("newmessage", user, message);
//        }

//        public async Task SendMessageToUser(string userId, string message)
//        {
//            if (UserConnections.TryGetValue(userId, out string connectionId))
//            {
//                string sender = Context.User.Identity.Name;
//                await Clients.Client(connectionId).SendAsync("ReceiveMessage", sender, message);
//            }
//        }
//    }
//}
