using ServiceHub.DAL.Entity;
using ServiceHub.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.BL.Interface
{
    public interface IServiceRepo
    {
        ApplicationUser FindEmail(string email);
        //Task SoftDelete(string id);
       
        Task<List<Order>> GetAllOrdersByUserId(int userId);
        //Task<List<Order>> GetAllOrdersByWorkerId(string workerId);
        Task<List<ApplicationUser>> GetAllWorkersByJobId(int jobId);

    }
}
