namespace API.Models.DTOs.Propuesta
{
    public class PropuestaDTO
    {
        public string Id { get; set; }
        public string UserId {  get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
    }
}
