using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WPF_CHEAT_os.Models;
using WPF_CHEAT_os.Utils;

namespace WPF_CHEAT_os.DTO
{
    public class PropuestaDTO
    {
        // OJO: los nombres en el API tienen que estar el mismo que estan a que impiezan de minisculas 
        // si no serialización/deserialización fallará.
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }
        [JsonPropertyName("fechaEnvio")]
        public DateTime FechaEnvio { get; set; }
        [JsonPropertyName("estado")]
        public EstadoPropuesta Estado { get; set; }
        [JsonPropertyName("alumno")]
        public AlumnoModel Alumno { get; set; }
        [JsonPropertyName("profesor")]
        public ProfesorModel Profesor { get; set; }
    }
}
