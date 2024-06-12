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
        public async Task<IActionResult> AddRateToWorker(RateDTO rateDTO)
        {
            try
            {
                var rate = mapper.Map<Rate>(rateDTO);
               await unitOfWork.RateRepo.AddRate(rate);
               await unitOfWork.saveAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
      
    }
}
