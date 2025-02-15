using API.Models.DTOs.Propuesta;
using API.Models.Entity;
using API.Repository;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropuestaController : BaseController<PropuestaEntity, PropuestaDTO, CreatePropuestaDTO>
    {
        public PropuestaController(IPropuestaRepository propuestaRepository,
            IMapper mapper, ILogger<PropuestaRepository> logger)
            : base(propuestaRepository, mapper, logger)
        {

        }
    }
}
