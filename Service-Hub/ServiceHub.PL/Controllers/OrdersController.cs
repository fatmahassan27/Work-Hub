//using Microsoft.AspNetCore.Mvc;
//using ServiceHub.BL.Interfaces;

//namespace ServiceHub.PL.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class OrdersController : ControllerBase
//    {
//        private readonly IUnitOfWork unit;

//        public OrdersController(IUnitOfWork unit)
//        {
//            this.unit = unit;
//        }

//        [HttpGet("user/{id}")]
//       // public async Task<IActionResult> GetAllOrdersByUserId(string id)
//        {
//           // return Ok(await unit.ServiceRepository.GetAllOrdersByUserId(id));
//        }
//    [HttpGet("worker/{id}")]
//    public async Task<IActionResult> GetAllOrdersByWorkerId(string id)
//    {
//        //return Ok(await unit.ServiceRepository.GetAllOrdersByWorkerId(id));
//    }
//}
//}
