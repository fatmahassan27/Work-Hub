using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTOs;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Interfaces;

namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork ;
        private readonly IMapper mapper;

        public RateController(IUnitOfWork unitOfWork,IMapper mapper) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddRate(RateDTO rateDTO)
        {
            try
            {
                var rate = mapper.Map<Rate>(rateDTO);
                await unitOfWork.RateRepo.AddRate(rate);
                await unitOfWork.saveAsync();
                return Ok("Rate Is Added Successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{workerId:int}")]
        public async Task<IActionResult> GetAllRatingsByWorkerId(int workerId)
        {
            try
            {
                var data = unitOfWork.RateRepo.GetAllRatingsByWorkerId(workerId);
                return Ok(data);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
      
    }
}
