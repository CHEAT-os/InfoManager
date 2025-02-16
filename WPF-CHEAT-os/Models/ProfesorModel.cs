using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_CHEAT_os.Models
{
    public class ProfesorModel
    {

        public int Id { get; set; }
        public string Nombre { get; set; }  
        public string Correo { get; set; }
        public string Especialidad { get; set; }
        public int HorasAsignadas { get; set; }
    }
}
