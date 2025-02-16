using API.Models.DTOs;
using System.Net;
using API.Models.DTOs.Propuesta;
using API.Models.DTOs.UserDto;
using API.Models.Entity;
using API.Repository;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Humanizer;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropuestaController : BaseController<PropuestaEntity, PropuestaDTO, CreatePropuestaDTO>
    {
        private readonly IPropuestaRepository _propuestaRepository;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        protected ResponseApi _reponseApi;
        public PropuestaController(IPropuestaRepository propuestaRepository, IMapper mapper, ILogger<PropuestaRepository> logger) : base(propuestaRepository, mapper, logger)
        {
            _propuestaRepository = propuestaRepository;
            _reponseApi = new ResponseApi();
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(Roles = "alumno,profesor")]
        [HttpPost("proponer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Proponer(CreatePropuestaDTO createPropuestaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _reponseApi.StatusCode = HttpStatusCode.BadRequest;
                    _reponseApi.IsSuccess = false;
                    _reponseApi.ErrorMessages.Add("Formato de propuesta inválido");
                    return BadRequest(_reponseApi);
                }

                var propuestaEntity = _mapper.Map<PropuestaEntity>(createPropuestaDto);
                var responsePropuesta = await _propuestaRepository.CreateAsync(propuestaEntity);

                _reponseApi.StatusCode = HttpStatusCode.OK;
                _reponseApi.IsSuccess = true;
                return Ok(_reponseApi);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Error al crear la propuesta:: " + ex.Message + ":\n" + ex.InnerException);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error creating propuesta: " + ex.Message);
            }
        }
    }
}