using ServiceHub.DAL.Entities;
using ServiceHub.DAL.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Interfaces
{
    public interface IChatMessageRepo : IGenericRepo<ChatMessage>
    {
        Task<IEnumerable<ChatMessage>> GetAllMessageByAnId(int id);

    }
}
