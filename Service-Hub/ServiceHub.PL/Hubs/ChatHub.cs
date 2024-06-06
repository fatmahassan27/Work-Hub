//using Microsoft.AspNetCore.SignalR;
//using ServiceHub.DAL.DataBase;
//using ServiceHub.DAL.Entity;

//namespace ServiceHub.PL.Hubs
//{
//	public class ChatHub :Hub
//	{
//        private readonly ApplicationDbContext db;

//        public ChatHub(ApplicationDbContext db) 
//		{
//            this.db = db;
//        }
	
       
//		public async Task  sendmessage(string messId , string message , string userId  , string workerId , string user )//path
//		{
//            var user1 = db.ApplicationUser.Where(a=>a.Id==Convert.ToInt32(userId)).FirstOrDefault();
//            var worker=db.Workers.Where(a=>a.Id==Convert.ToInt32(workerId)).FirstOrDefault();

//            ChatMessage _message = new ChatMessage()
//            {
//                Message = message,
//                Id = int.Parse(messId),
//                WorkerId= int.Parse(workerId),
//                UserId=int.Parse(userId)

//            };
//            db.ChatMessage.Add(_message);
//            db.SaveChanges();
//            Clients.All.SendAsync("newmessage", user , message);
//		}
//	}
//}
