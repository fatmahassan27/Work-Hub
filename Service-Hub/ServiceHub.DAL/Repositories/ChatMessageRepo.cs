using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ServiceHub.DAL.DataBase;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.GenericRepository;
using ServiceHub.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.DAL.Repositories
{
    public class ChatMessageRepo :GenericRepo<ChatMessage>,IChatMessageRepo
    {
        private readonly ApplicationDbContext db;

        public ChatMessageRepo(ApplicationDbContext db ):base(db)
        {
            this.db = db;
        }

        public  async Task<IEnumerable<ChatMessage>> GetAllMessages(int senderId, int receiverId)
        {
            try
            {


                var messages = await db.ChatMessage
                    .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) || (m.SenderId == receiverId && m.ReceiverId == senderId))
                    .OrderBy(m => m.createdDate)
                    .ToListAsync();
                await Console.Out.WriteLineAsync($"HIIIIIIIIIIIIIIII{messages}");


                if (messages == null || !messages.Any())
                {
                    Console.WriteLine("No messages found.");
                }
                else
                {
                    Console.WriteLine($"{messages.Count} messages found.");
                }

                return messages;


            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }

      
                


        }
    }
}
