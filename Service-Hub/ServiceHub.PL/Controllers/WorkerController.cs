﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTO;
using ServiceHub.BL.UnitOfWork;
using ServiceHub.DAL.Entity;
using ServiceHub.DAL.Helper;

namespace ServiceHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly UnitWork unitWork;

        public WorkerController(UnitWork unitWork)
        {
            this.unitWork = unitWork;
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await unitWork.AppUserRepo.GetAll();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        WorkerDTO workerDTO = new WorkerDTO()
                        {
                            Id = item.Id,
                            FullName = item.UserName,
                            Email = item.Email,
                            JobId = item.JobId,
                            DistrictId = item.DistrictId,
                            Rating = item.Rating

                        };
                        return Ok(workerDTO);
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log exception (ex) here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }

        }
        [HttpGet("{id:int}")]
        //[Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var data = await unitWork.AppUserRepo.GetById(id);
                if (data != null)
                {
                    WorkerDTO workerDTO = new WorkerDTO()
                    {
                        Id = data.Id,
                        FullName = data.UserName,
                        Email = data.Email,
                        JobId = data.JobId,
                        DistrictId = data.DistrictId,
                        Rating = data.Rating

                    };

                    return Ok(workerDTO);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log exception (ex) here if needed
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }
        [HttpPost]
        public IActionResult Add(WorkerDTO worker)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appWorker = new ApplicationUser()
                {
                    UserName = worker.FullName,
                    Email = worker.Email,
                    PasswordHash = worker.Password,
                    JobId = worker.JobId,
                    DistrictId = worker.DistrictId,
                    Rating = worker.Rating
                };
                unitWork.AppUserRepo.Create(appWorker);
                unitWork.saveChanges();
                return Created();
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] WorkerDTO workerDTO)
        {

            var worker = await unitWork.AppUserRepo.GetById(id);
            if (worker == null)
            {
                return NotFound();
            }


            worker.UserName = workerDTO.FullName;
            worker.Email = workerDTO.Email;
            worker.PasswordHash = workerDTO.Password;
            worker.JobId = workerDTO.JobId;
            worker.DistrictId = workerDTO.DistrictId;
            worker.Rating = workerDTO.Rating;
            await unitWork.AppUserRepo.Update(id, worker);
            unitWork.saveChanges();

            return Ok(worker);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var worker = unitWork.AppUserRepo.GetById(id);
            if (worker == null) return NotFound();
            await unitWork.AppUserRepo.Delete(id);
            unitWork.saveChanges();
            return Ok("Deleted");
        }

        //api/worker/job/{jobId}
        [HttpGet("job/{jobId}")]
        ////[Authorize]
        public async Task<IActionResult> GetByJobId(int jobId)
        {
            try
            {
                var data = await unitWork.ServiceRepository.GetAllWorkersByJobId(jobId);
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        WorkerDTO workerDTO = new WorkerDTO()
                        {
                            Id = item.Id,
                            FullName = item.UserName,
                            Email = item.Email,
                            JobId = item.JobId,
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
