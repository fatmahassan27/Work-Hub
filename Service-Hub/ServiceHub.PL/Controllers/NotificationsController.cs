using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.Interfaces;

namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAllNotificationsbyOwnerId(int id)
        {
            try
            {
                var notifications = await this.notificationService.GetAllNotificationsByOwnerId(id);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("error getting notifications: "+ex.Message);
                return BadRequest(ex.Message);
            }
           
        }
      

    }
}
