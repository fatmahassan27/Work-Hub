using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTOs;
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
        [HttpGet("{id:int}")]//userId
        public async Task<IActionResult> GetAllMessages(int id)
        {
            try
            {
                 var data =chatService.GetAllMessageByAnId(id);
                 return Ok(data);

            } catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("error get messages: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
