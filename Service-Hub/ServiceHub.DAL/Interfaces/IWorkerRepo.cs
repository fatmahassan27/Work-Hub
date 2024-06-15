using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Interfaces
{
    public interface IWorkerRepo  
    {

        Task<double> getAverageWorkerRating(int workerId);
       
    }
}
