using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models.Entity
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public ICollection<PropuestaEntity> Propuestas { get; set; } = new List<PropuestaEntity>();
        public ICollection<CursoEntity> Cursos { get; set; } = new List<CursoEntity>();
        public ICollection<AsignaturaEntity> Asignaturas { get; set; } = new List<AsignaturaEntity>();
    }
}
