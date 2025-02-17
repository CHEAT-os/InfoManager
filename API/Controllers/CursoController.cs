using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTOs.Curso;
using API.Repository;
using API.Models.Entity;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : BaseController<CursoEntity, CursoDTO, CreateCursoDTO>
    {
        public CursoController(ICursoRepository cursoRepository,
            IMapper mapper, ILogger<CursoController> logger)
            : base(cursoRepository, mapper, logger)
        {

        }
    }
}