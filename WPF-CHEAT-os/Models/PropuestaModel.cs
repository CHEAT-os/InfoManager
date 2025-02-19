
using WPF_CHEAT_os.Utils;

namespace WPF_CHEAT_os.Models
{
    public class PropuestaModel
    {

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEnvio { get; set; }
        public EstadoPropuesta Estado { get; set; }
        public AlumnoModel Alumno { get; set; }
        public ProfesorModel Profesor { get; set; }

    }
}
