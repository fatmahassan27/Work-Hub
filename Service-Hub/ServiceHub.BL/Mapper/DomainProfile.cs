
using AutoMapper;
using ServiceHub.BL.DTOs;
using ServiceHub.DAL.Entities;

namespace ServiceHub.BL.Mapper
{
    public class DomainProfile :Profile
    {
        public DomainProfile()
        {
            //entity to dto
            CreateMap<Job, JobDTO>();

            //dto to entity
            CreateMap<JobDTO, Job>(); 
             
        }
    }
}
