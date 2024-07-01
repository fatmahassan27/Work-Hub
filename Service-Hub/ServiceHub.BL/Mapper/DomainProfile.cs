﻿
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
            CreateMap<District, DistrictDTO>();
            CreateMap<DistrictDTO, District>();
            //--------------------------------------------
            CreateMap<Rate, RateDTO>();
            CreateMap<RateDTO, Rate>();
            //--------------------------------------------
            CreateMap<Notification, NotificationDTO>();
            CreateMap<NotificationDTO, Notification>();
            //--------------------------------------------
            CreateMap<ChatDTO, ChatMessage>();
            CreateMap<ChatMessage, ChatDTO>()
          .ForMember(dest => dest.createdDate, opt => opt.MapFrom(src => src.createdDate));
        }
    }
}
