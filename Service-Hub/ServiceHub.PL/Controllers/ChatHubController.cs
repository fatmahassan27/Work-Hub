using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.Interface;
using ServiceHub.BL.UnitOfWork;
using ServiceHub.DAL.Entity;

namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatHubController : ControllerBase
    {
        private readonly UnitWork unitOWork;

        private readonly IBaseRepo<ChatMessage> chat;

        public ChatHubController(UnitWork unitOWork)
        {
            this.unitOWork = unitOWork;
        }
    }
}
