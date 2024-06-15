using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;

namespace ServiceHub.PL.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DistrictController : ControllerBase
	{
        private readonly IDistrictService districtService;
        private readonly IMapper mapper;

		public DistrictController(IDistrictService districtService, IMapper mapper)
		{
            this.districtService = districtService;
            this.mapper = mapper;
		}
		[HttpGet("{cityId:int}")]
		public async Task<ActionResult<IEnumerable<DistrictDTO>>> GetAllByCityId(int cityId)
		{
			try
			{
				var districts = await districtService.GetAllByCityId(cityId);
				if (districts != null)
				{
					var DistrictDTO = mapper.Map<IEnumerable<DistrictDTO>>(districts);
					return Ok(DistrictDTO);
				}
				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new
				{
					message = "An error occurred while retrieving Districts",
					details = ex.Message
				});
			}
		}
	}
}
