using System.ComponentModel.DataAnnotations;

namespace API.Models.Entity
{
    public class AsignaturaEntity
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<User> Users { get; } = [];
        public CursoEntity Curso { get; }
    }
}
