using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;

namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost("{userId:int}/{workerId:int}")]
        public async Task<IActionResult> CreateOrder(int userId, int workerId)
        {
            try
            {
                await orderService.CreateOrderAsync(userId, workerId);
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error Creating Order : {ex}");

            }
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetAllOrdersByUserId(int id)
        {
            try
            {
                var ordersDTO = await orderService.GetAllOrdersByUserId(id);
                return Ok(ordersDTO);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data : {ex}");

            }
        }
        [HttpGet("worker/{id}")]
        public async Task<IActionResult> GetAllOrdersByWorkerId(int id)
        {
            try
            {
                var ordersDTO = await orderService.GetAllOrdersByWorkerId(id);
                return Ok(ordersDTO);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving Order : {ex}");
            }

        }
        [HttpPut("{id:int}/{status:int}")]
         public async Task<IActionResult> Update(int id,int status)
         {
            try
            { 
                  await orderService.UpdateOrderAsync(id, status);
                  return Ok("Updated");
                 
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving Order : {ex}");
            }
         }

    }
}
