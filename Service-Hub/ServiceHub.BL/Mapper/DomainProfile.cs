
using AutoMapper;
using ServiceHub.BL.DTOs;
using ServiceHub.DAL.Entities;
using ServiceHub.DAL.Helper;

namespace ServiceHub.BL.Mapper
{
    public class DomainProfile :Profile
    {
        public DomainProfile()
        {
            CreateMap<Job, JobDTO>();
            CreateMap<JobDTO, Job>();
            //--------------------------------------------
            CreateMap<ApplicationUser, RegistrationDTO>();
            CreateMap<RegistrationDTO, ApplicationUser>();
            //--------------------------------------------
            CreateMap<ApplicationUser, WorkerDTO>();
            CreateMap<WorkerDTO, ApplicationUser>();
            //--------------------------------------------
            CreateMap<Order, OrderDTO>();
            CreateMap<OrderDTO, Order>();
            //--------------------------------------------
            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();
            //--------------------------------------------

            //--------------------------------------------

        }
    }
}
