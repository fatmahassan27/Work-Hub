using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;

namespace ServiceHub.PL.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CityController : ControllerBase
	{
		private readonly ICityService CityService;
	
		public CityController(ICityService _cityService)
		{
			CityService = _cityService;
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult<IEnumerable<CityDTO>>> Getall()
		{
			try
			{
				var CityDTOs = await CityService.GetAllCity();
				if (CityDTOs != null)
				{
					return Ok(CityDTOs);
				}
				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while retrieving Citites", details = ex.Message });
			}
		}
	}
}
