using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.Curso
{
    public class CreateCursoDTO
    {
        [Required(ErrorMessage = "Field required: Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Field required: Turno")]
        public string Turno { get; set; }
        public List<int>? UserIds { get; set; }
        public List<int>? AsignaturaIds { get; set; }
    }
}
