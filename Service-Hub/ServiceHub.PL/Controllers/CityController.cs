using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Services;
using ServiceHub.BL.UnitOfWork;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Interfaces;

namespace ServiceHub.PL.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CityController : ControllerBase
	{
		private readonly ICityService CityService;
		private readonly IMapper mapper;
	
		public CityController(ICityService _cityService, IMapper mapper)
		{
			CityService = _cityService;
			this.mapper = mapper;
		}
	
		[HttpGet("GetAllCity")]
		public async Task<ActionResult<IEnumerable<CityDTO>>> Getall()
		{
			try
			{
				var cities = await CityService.GetAllCity();
				if (cities != null)
				{
					var CityDTOs= mapper.Map<IEnumerable<CityDTO>>(cities);
					return Ok(CityDTOs);
				}
				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving the jobs.", details = ex.Message });
			}
		}
	}
}
