
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTOs;
using ServiceHub.DAL.Helper;


namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public WorkerController( UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpGet("GetAllWorker")]
        //[Authorize(Policy = "Worker")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await userManager.GetUsersInRoleAsync("Worker");
                if (data != null)
                {
                    var listDTOs = mapper.Map<IEnumerable<WorkerDTO>>(data);
                   
                    return Ok(listDTOs);  

                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $" Error retrieving data :{ex}");
            }

        }
        [HttpGet("GetWorkerById/{id:int}")]
        //[Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var data = await userManager.FindByIdAsync(id.ToString()); ;
                if (data != null)
                {
                    WorkerDTO workerDTO = new WorkerDTO()
                    {
                        Id = data.Id,
                        FullName = data.UserName,
                        Email = data.Email,
                        JobId = data.JobId,
                        DistrictId = data.DistrictId, // Handling nullable int
                        Rating = data.Rating
                    };
                    return Ok(workerDTO);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }

        [HttpPut("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] WorkerDTO workerDTO)
        {
            try
            {
                var worker = await userManager.FindByIdAsync(id.ToString());
                if (worker != null)
                {
                    worker.UserName = workerDTO.FullName;
                    worker.Email = workerDTO.Email;
                    worker.DistrictId = workerDTO.DistrictId;
                    worker.JobId = workerDTO.JobId;
                    worker.Rating = workerDTO.Rating;
                    var result = await userManager.UpdateAsync(worker);
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
        }
        [HttpDelete("DeleteById/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var worker = await userManager.FindByIdAsync(id.ToString());
                if (worker == null) return NotFound();
                await userManager.DeleteAsync(worker);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data :{ex}");
            }

        }

        //[Authorize]
        [HttpGet("WorkersByJobId/{jobId:int}")]
        public async Task<IActionResult> GetAllByJobId(int jobId)
        {
            try
            {
                var data = userManager.Users.Where(a => a.JobId == jobId);
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        WorkerDTO workerDTO = new WorkerDTO()
                        {
                            Id = item.Id,
                            FullName = item.UserName,
                            Email = item.Email,
                            DistrictId = item.DistrictId,
                            Rating = item.Rating,
                        };
                        return Ok(workerDTO);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data, Message: {ex}");
            }
        }

    }
}

