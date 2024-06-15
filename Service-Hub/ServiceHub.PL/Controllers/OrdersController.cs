using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTOs;
using ServiceHub.DAL.UnitOfWork;

namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public OrdersController(IUnitOfWork unit , IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpPost("{userId:int}/{workerId:int}")]
        public async Task<IActionResult> CreateOrder(int userId, int workerId)
        {
            try
            {
                await unit.OrderRepo.CreateOrderAsync(userId, workerId);
                await unit.saveAsync();
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
                var orders = await unit.OrderRepo.GetAllOrdersByUserId(id);
                var result = mapper.Map<IEnumerable<OrderDTO>>(orders);
                return Ok(result);
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
                var orders = await unit.OrderRepo.GetAllOrdersByWorkerId(id);
                var result = mapper.Map<IEnumerable<OrderDTO>>(orders);
                return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving Order : {ex}");

            }

        }
        [HttpPut("{id:int}/{status:int}")]
         public async Task<IActionResult> Update(int id,int status)
         {
            try
            { 
                  await unit.OrderRepo.UpdateOrderAsync(id,status);
                  await unit.saveAsync();
                  return Ok("Updated");
                 
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving Order : {ex}");

            }

         }

    }
}
