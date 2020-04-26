using AutoMapper;
using EasyKBTaskBoard.API.Entities;
using EasyKBTaskBoard.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyKBTaskBoard.API.Profiles
{
    public class ColumnProfile : Profile
    {
        public ColumnProfile()
        {
            CreateMap<Column, ColumnDto>();
            CreateMap<ColumnForCreationDto, Column>();
            CreateMap<ColumnForUpdateDto, Column>().ReverseMap();
        }
    }
}
