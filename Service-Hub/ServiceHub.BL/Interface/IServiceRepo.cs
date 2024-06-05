﻿using ServiceHub.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.BL.Interface
{
    public interface IServiceRepo
    {
       User FindEmail(string email);
       Worker Findemail(string email);
       Task SoftDelete(int id);
       Task<List<Order>> GetAllOrdersByUserId(int userId);
       Task<List<Order>> GetAllOrdersByWorkerId(int workerId);
       Task<List<Worker>> GetAllByJobId(int jobId);




    }
}
