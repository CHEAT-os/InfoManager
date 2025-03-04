using System.Collections.Generic;

namespace WPF_CHEAT_os.Models
{
    public class PropuestaModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Email { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string Profesor1 { get; set; }
        public string Profesor2 { get; set; }
        public string Profesor3 { get; set; } 
        public List<string> UserIds { get; set; } = new List<string>();
    }
}
