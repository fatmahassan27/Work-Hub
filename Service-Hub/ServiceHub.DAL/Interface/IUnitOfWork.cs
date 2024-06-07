﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Interface
{
    public interface IUnitOfWork 
    {
        IOrderRepo CustomOrderRepo { get; }

        Task<int> saveChanges();
    }
}
