using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.Propuesta
{
    public class CreatePropuestaDTO : PropuestaDTO
    {
        [Required(ErrorMessage = "Field required: username")]
        public string username { get; set; }
        [Required(ErrorMessage = "Field required: Titulo")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Field required: Descripcion")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Field required: Tipo")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Field required: Estado")]
        public string Estado { get; set; }
    }
}
