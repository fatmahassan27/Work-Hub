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
        [HttpGet("{senderId:int}/{receiverId:int}")]
        public async Task<IActionResult> GetAllMessages(int senderId, int receiverId)
        {
            try
            {
                var data = await chatService.GetAllMessages(senderId,receiverId);
                if(data==null)
                {
                    return NotFound("No messages found");
                }
                Console.WriteLine("sdfghjkl",data);
                 return Ok(data);

            } catch (Exception ex)
            {
                Console.Error.WriteLine("Error getting messages: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] ChatDTO chatMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await chatService.CreateMessage(chatMessage);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error creating message: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
