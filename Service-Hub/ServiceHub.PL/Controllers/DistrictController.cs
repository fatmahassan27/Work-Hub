using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTOs;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Interfaces;

namespace ServiceHub.PL.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DistrictController : ControllerBase
	{
		private readonly IDistrictService DistrictService;
		private readonly IMapper mapper;


		public DistrictController(IDistrictService _DistrictService, IMapper mapper)
		{
			DistrictService = _DistrictService;
			this.mapper = mapper;
		}
		[HttpGet("all")]
		public async Task<ActionResult<IEnumerable<DistrictDTO>>> GetAllByCityId(int cityid)
		{
			try
			{
				var districts = await DistrictService.GetAllByCityId(cityid);
				if (districts == null)

				{
					var DistrictDTO = mapper.Map<DistrictDTO>(districts);
					return Ok(DistrictDTO);
				}

				return NotFound();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new
				{
					message = "An error occurred while retrieving the jobs.",
					details = ex.Message
				});

			}
			

		}
	}
}
