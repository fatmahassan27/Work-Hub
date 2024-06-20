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

		public DistrictController(IDistrictService districtService)
		{
            this.districtService = districtService;
		}
		[HttpGet]
        public async Task<ActionResult<IEnumerable<DistrictDTO>>> GetAll()
        {
            try
            {
                var districtsDTO = await districtService.GetAll();
                if (districtsDTO != null)
                {
                    return Ok(districtsDTO);
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

        [HttpGet("{cityId:int}")]
		public async Task<ActionResult<IEnumerable<DistrictDTO>>> GetAllByCityId(int cityId)
		{
			try
			{
				var districtsDTO = await districtService.GetAllDistrictsByCityId(cityId);
				if (districtsDTO != null)
				{
					return Ok(districtsDTO);
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

		[HttpGet("byId/{id:int}")]
		public async Task<IActionResult> getById(int id)
		{
			try
			{
                var districtDTO =  await districtService.getById(id);
				if (districtDTO != null)
					return Ok(districtDTO);
				return NotFound();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while retrieving District",
                    details = ex.Message
                });
            }

        }
	}
}
