using ServiceHub.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHub.BL.DTO
{
    public class JobDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        /// <summary>
        /// Converts a Job entity to JobDTO.
        /// </summary>
        /// <param name="job">The Job entity.</param>
        /// <returns>The JobDTO.</returns>
        public static JobDTO FromJob(Job job)
        {
            if (job == null) throw new ArgumentNullException(nameof(job));
            return new JobDTO
            {
                Id = job.Id,
                Name = job.Name,
                Price = job.Price
            };
        }

        /// <summary>
        /// Converts a list of Job entities to a list of JobDTOs.
        /// </summary>
        /// <param name="jobs">The list of Job entities.</param>
        /// <returns>The list of JobDTOs.</returns>
        public static List<JobDTO> FromJobs(List<Job> jobs)
        {
            if (jobs == null) throw new ArgumentNullException(nameof(jobs));
            return jobs.Select(FromJob).ToList();
        }

        /// <summary>
        /// Converts a list of JobDTOs to a list of Job entities.
        /// </summary>
        /// <param name="jobDtos">The list of JobDTOs.</param>
        /// <returns>The list of Job entities.</returns>
        public static List<Job> ToJobs(List<JobDTO> jobDtos)
        {
            if (jobDtos == null) throw new ArgumentNullException(nameof(jobDtos));
            return jobDtos.Select(ToJob).ToList();
        }

        /// <summary>
        /// Converts a JobDTO to a Job entity.
        /// </summary>
        /// <param name="jobDto">The JobDTO.</param>
        /// <returns>The Job entity.</returns>
        public static Job ToJob(JobDTO jobDto)
        {
            if (jobDto == null) throw new ArgumentNullException(nameof(jobDto));
            return new Job
            {
                Id = jobDto.Id,
                Name = jobDto.Name,
                Price = jobDto.Price
            };
        }
    }
}
