namespace API.Models.DTOs.Asignatura
{
    public class CreateAsignaturaDTO
    {
        public int CursoId { get; set; }
        public string Nombre { get; set; }
        public int Horas { get; set; }
    }
}
