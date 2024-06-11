using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(int wokerId, int userId);

    }
}
