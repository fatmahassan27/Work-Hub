using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceHub.BL.DTO;
using ServiceHub.BL.UnitOfWork;
using ServiceHub.DAL.Entity;

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
        public async Task< IActionResult> GetAll()
        {
            var data =  await unitWork.WorkerRepo.GetAll();
            return Ok(data);
        }
        [HttpGet("{id:int}")]
        //[Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var data = await unitWork.WorkerRepo.GetById(id);
                if (data != null)
                {
                    return Ok(data);
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
                Worker appWorker = new Worker()
                {
                    FullName = worker.FullName,
                    Email = worker.Email,
                    Password = worker.Password,
                    JobId = worker.JobId,
                    DistrictId = worker.DistrictId
                };
                unitWork.WorkerRepo.Create(appWorker);
                unitWork.saveChanges();
                return Created();
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id , [FromBody] WorkerDTO workerDTO)
        {
            
                var worker = await unitWork.WorkerRepo.GetById(id);
                if (worker == null)
                {
                    return NotFound();
                }

                worker.FullName = workerDTO.FullName;
                worker.Email = workerDTO.Email;
                worker.Password = workerDTO.Password;
                worker.JobId = workerDTO.JobId;
                worker.DistrictId = workerDTO.DistrictId;

                 await unitWork.WorkerRepo.Update(id, worker);
                 unitWork.saveChanges();

                return Ok(worker);
           
        }

        [HttpDelete("{id:int}")]
        public  async Task<IActionResult> Delete(int id)
        {
            //var worker  = unitWork.WorkerRepo.GetById(id);
            //if (worker == null) return NotFound();
           await  unitWork.ServiceRepository.SoftDelete(id);
            unitWork.saveChanges();
            return Ok("Deleted");
        }
        
    }
}
