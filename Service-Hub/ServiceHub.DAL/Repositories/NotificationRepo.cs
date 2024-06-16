using Microsoft.EntityFrameworkCore;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.GenericRepository;
using ServiceHub.DAL.Interfaces;

namespace ServiceHub.DAL.Repositories
{
    public class NotificationRepo : GenericRepo<Notification> , INotificaionRepo
    {
        public NotificationRepo(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsByOwnerId(int ownerId)
        {
            var notifications = await db.Notifications.Where(x => x.OwnerId == ownerId).ToListAsync();
            return notifications;

        }
    }
}
