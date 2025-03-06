using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using WPF_CHEAT_os.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPF_CHEAT_os.Models
{
    public class AsignarProfePorpuestaModel
    {
        public int UserId { get; set; }
        public int PropuestaId { get; set; }

        internal static AsignarProfePorpuestaModel CreateModelFromDTO(AsignarPropuestaDTO asignar)
        {
            return new AsignarProfePorpuestaModel
            {
               UserId = asignar.UserId,
               PropuestaId = asignar.PropuestaId,
            };

        }
    }

}
