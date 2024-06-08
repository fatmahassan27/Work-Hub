using ServiceHub.BL.Interface;
using ServiceHub.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Interface
{
    public interface IUnitOfWork 
    {
        IOrderRepo CustomOrderRepo { get; }
        IGenericRepo<ApplicationUser> UserApp { get; }

        Task<int> saveChanges();
    }
}
