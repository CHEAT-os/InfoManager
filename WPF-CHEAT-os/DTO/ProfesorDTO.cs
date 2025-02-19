using System.Text.Json.Serialization;


namespace WPF_CHEAT_os.DTO
{
    public class ProfesorDTO
    {
        // OJO: los nombres en el API tienen que estar el mismo que estan a que impiezan de minisculas 
        // si no serialización/deserialización fallará.
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; }
        [JsonPropertyName("correo")]
        public string Correo { get; set; }
        [JsonPropertyName("especialidad")]
        public string Especialidad { get; set; }
        [JsonPropertyName("horasAsignadas")]
        public int HorasAsignadas { get; set; }
    }
}
