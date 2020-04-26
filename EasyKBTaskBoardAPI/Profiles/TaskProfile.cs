using AutoMapper;
using EasyKBTaskBoard.API.Models;
using EasyKBTaskBoard.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Entities.Task, TaskDto>();
            CreateMap<TaskForCreationDto, Entities.Task>();
            CreateMap<TaskForUpdateDto, Entities.Task>().ReverseMap();
        }
    }
}
