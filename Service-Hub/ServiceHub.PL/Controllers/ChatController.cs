using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.Interfaces;
using ServiceHub.DAL.Helper;

namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatMessageService chatService;

        public ChatController(IChatMessageService chatService)
        {
            this.chatService = chatService;
        }

    }
}
