﻿
using API.Models.DTOs.Curso;
using API.Models.DTOs.Propuesta;
using API.Models.DTOs.UserDto;
using API.Models.Entity;
using AutoMapper;


namespace API.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {


            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<PropuestaEntity, PropuestaDTO>().ReverseMap();
            CreateMap<CreatePropuestaDTO, PropuestaEntity>().ReverseMap();
            CreateMap<CursoEntity, CursoDTO>().ReverseMap();
            CreateMap<CreateCursoDTO, CursoEntity>().ReverseMap();
        }
    }
}
