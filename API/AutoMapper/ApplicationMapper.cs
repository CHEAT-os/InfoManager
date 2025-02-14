
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
            

            //CreateMap<HouseEntity, HouseDTO>().ReverseMap();
            //CreateMap<CreateHouseDTO, HouseEntity>().ReverseMap();


            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<PropuestaEntity, PropuestaDTO>().ReverseMap();
            CreateMap<PropuestaDTO, CreatePropuestaDTO>().ReverseMap();

        }
    }
}
