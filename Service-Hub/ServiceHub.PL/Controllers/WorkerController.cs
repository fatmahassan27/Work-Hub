using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTOs;
using ServiceHub.BL.Interfaces;


namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService workerService;

        public WorkerController(IWorkerService workerService)
        {
            this.workerService = workerService;
        }

        [HttpGet]
        //[Authorize(Policy = "Worker")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var workersDTO = await workerService.GetAllWorkers();
                return Ok(workersDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $" Error retrieving data :{ex}");
            }

        }
        [HttpGet("{id:int}")]
        //[Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var workerDTO = await workerService.GetWorkerById(id);
                if(workerDTO != null)
                    return Ok(workerDTO);
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex}");
            }
        }

        /*
        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] WorkerDTO workerDTO)
        {
            try
            {
                var worker = await userManager.FindByIdAsync(id.ToString());
                if (worker != null)
                {
                    var appuser = mapper.Map<ApplicationUser>(workerDTO); 

                    var result = await userManager.UpdateAsync(appuser);
                    if (result.Succeeded)
                    {
                        return Ok("Updated");
                    }
                    return BadRequest(result.Errors);

                }
                return NotFound();
            
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating data: {ex.Message}");
            }
        }*/

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await workerService.DeleteWorker(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data :{ex}");
            }

        }

        //[Authorize]
        [HttpGet("Job/{jobId:int}")]
        public async Task<IActionResult> GetAllByJobId(int jobId)
        {
            try
            {
                var workersDTO = await workerService.GetAllWorkersByJobId(jobId);
                return Ok(workersDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data, Message: {ex}");
            }
        }



        [HttpGet("District/{id:int}")]
        public async Task<IActionResult> GetByDistrictId(int id)
        {
            try
            {
                var workersDTO = await workerService.GetAllWorkersByDistrictId(id);
                return Ok(workersDTO);
            }

            catch (Exception ex)
            {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data, Message: {ex}");
            }
        }


    }
}

