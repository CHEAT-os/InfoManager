
using API.Models.DTOs.Propuesta;
using API.Models.Entity;
using AutoMapper;


namespace API.AutoMapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<PropuestaEntity, PropuestaDTO>().ReverseMap();
            CreateMap<PropuestaDTO, CreatePropuestaDTO>().ReverseMap();

        }
    }
}
