using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WPF_CHEAT_os.DTO
{
    public class PropuestaDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("titulo")]
        public string Titulo { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; }

        [JsonPropertyName("users")]
        public ICollection<UsuarioDTO> Users { get; } = [];
    }
}
