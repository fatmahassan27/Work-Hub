//using Microsoft.AspNetCore.SignalR;
//using ServiceHub.DAL.DataBase;
//using ServiceHub.DAL.Entity;

//namespace ServiceHub.PL.Hubs
//{
//    public class ChatHub : Hub
//    {
//        private readonly ApplicationDbContext db;

//        public ChatHub(ApplicationDbContext db)
//        {
//            this.db = db;
//        }


//        public async Task sendmessage(string messId, string message, string userId, string workerId, string user)//path
//        {
//            var user1 = db.ApplicationUsers.Where(a => a.Id == userId).FirstOrDefault();
//            var worker = db.ApplicationUsers.Where(a => a.Id == workerId).FirstOrDefault();

//            ChatMessage _message = new ChatMessage()
//            {
//                Message = message,
//                Id = int.Parse(messId),
//                WorkerId =workerId,
//                UserId =userId

//            };
//            db.ChatMessage.Add(_message);
//            db.SaveChanges();
//            Clients.All.SendAsync("newmessage", user, message);
//        }
//    }
//}
