using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.Entity
{
    public class AsignaturaEntity
    {
        [Key]
        public int Id { get; set; }
        public int CursoId { get; set; } 
        public string Nombre { get; set; }
        public int Horas { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public CursoEntity Curso { get; set; }
    }
}
