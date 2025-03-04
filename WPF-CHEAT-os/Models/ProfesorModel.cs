
namespace WPF_CHEAT_os.Models
{
    public class ProfesorModel
    {
        public string Id { get; set; }
        public string Nombre { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
