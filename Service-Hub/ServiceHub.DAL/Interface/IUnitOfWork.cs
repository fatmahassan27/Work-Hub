using ServiceHub.BL.Interface;
using ServiceHub.DAL.Entity;
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
        IGenericRepo<Job> JobRepo { get; }

        Task<int> saveAsync();
    }
}
