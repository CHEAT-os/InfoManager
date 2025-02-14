using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.Propuesta
{
    public class CreatePropuestaDTO : PropuestaDTO
    {
        [Required(ErrorMessage = "Field required: Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Field required: UserId")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Field required: Titulo")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Field required: UserId")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Field required: Tipo")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Field required: Estado")]
        public string Estado { get; set; }
    }
}
