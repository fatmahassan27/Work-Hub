using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTO;
using ServiceHub.DAL.Entity;
using ServiceHub.DAL.Helper;
using Microsoft.EntityFrameworkCore;


namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;


        public WorkerController( UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet("GetAllWorker")]
        //[Authorize(Policy = "Worker")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await userManager.GetUsersInRoleAsync("worker");
                if (data != null)
                {
                    var workerDTOs = data.Select(item => new WorkerDTO
                    {
                        FullName = item.UserName,
                        Email = item.Email,
                        JobId = item.Job.Id,
                        DistrictId = (int)item.DistrictId,
                        Rating = item.Rating
                    }).ToList();
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
                        FullName = data.UserName,
                        Email = data.Email,
                        JobId = data.Job.Id,
                        DistrictId = data.DistrictId.HasValue ? data.DistrictId.Value : 0, // Handling nullable int
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
                if (worker == null)
                {
                    return NotFound();
                }
                ApplicationUser appworker = new ApplicationUser()
                {
                    UserName = workerDTO.FullName,
                    Email = workerDTO.Email,
                    JobId = workerDTO.JobId,
                    DistrictId = workerDTO.DistrictId,
                    Rating = workerDTO.Rating,

                };
                await userManager.UpdateAsync(appworker);
                return Ok(appworker);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data :{ex}");

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

