using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WPF_CHEAT_os.DTO
{
    // OJO: los nombres en el API tienen que estar el mismo que estan a que impiezan de minisculas 
    // si no serialización/deserialización fallará.
    public class AlumnoDTO
    {
        [JsonPropertyName("id")]
        public int id { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }
        [JsonPropertyName("correo")]
        public string Correo { get; set; }
        [JsonPropertyName("cicloFormativo")]
        public string CicloFormativo { get; set; }
    }
}
