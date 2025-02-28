using API.Models.DTOs.Asignatura;
using API.Models.DTOs.Curso;
using API.Models.Entity;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignaturaController : BaseController<AsignaturaEntity, AsignaturaDTO, CreateAsignaturaDTO>
    {
        public AsignaturaController(IAsignaturaRepository asignaturaRepository,
            IMapper mapper, ILogger<AsignaturaController> logger)
            : base(asignaturaRepository, mapper, logger)
        {

        }
    }
}
