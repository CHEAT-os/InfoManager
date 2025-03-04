using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Entity
{
    public class CursoEntity
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Turno { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<AsignaturaEntity> Asignaturas { get; set; } = new List<AsignaturaEntity>();
    }
}
