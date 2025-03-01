using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_CHEAT_os.DTO;

namespace WPF_CHEAT_os.Interfaces
{
    public interface IAsignarProvider
    {
        List<AlumnoDTO> ListaClean(List<AlumnoDTO> listaAprobados, List<AlumnoDTO> ListaAsignada);
        List<AlumnoDTO> AlumnosAsignados(List<AlumnoDTO> listaAprobados, int maxAlumnos);
        int TutorPuedeLlevar(int horas, int alumnos);
    }
}
