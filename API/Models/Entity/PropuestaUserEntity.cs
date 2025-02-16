using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models.Entity
{
    public class PropuestaUserEntity
    {
        public int PropuestaId { get; set; }
        public int UserId { get; set; }
        public PropuestaEntity Propuesta { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}