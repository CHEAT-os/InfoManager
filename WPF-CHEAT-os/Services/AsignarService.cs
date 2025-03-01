using System;
using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Interfaces;

namespace WPF_CHEAT_os.Services
{
    public class AsignarService : IAsignarProvider
    {
        private const int HORAS_PROFES_TOTALES = 540;
        public AsignarService() { }

        // Número de alumnos entre número de horas totales y multiplicado por la hora de cada profesor
        public int TutorPuedeLlevar(int horas, int alumnos)
        {
            // Calcular el número de alumnos que puede llevar el tutor
            int alumnosPermitidos = (alumnos / HORAS_PROFES_TOTALES) * horas;

            return alumnosPermitidos;
        }

        // Asigna alumnos de manera aleatoria
        public List<AlumnoDTO> AlumnosAsignados(List<AlumnoDTO> listaAprobados, int maxAlumnos)
        {
            // Mezclar la lista de alumnos aleatoriamente
            Random r = new Random();
            List<AlumnoDTO> listaMezclada = listaAprobados.OrderBy(x => r.Next()).ToList();

            // Tomar solo el número máximo de alumnos permitido
            return listaMezclada.Take(maxAlumnos).ToList();
        }

        // Metodo para eliminar de la lista de aprobados los alumnos que ya estan asignados a otro profesor
        public List<AlumnoDTO> ListaClean(List<AlumnoDTO> listaAprobados, List<AlumnoDTO> ListaAsignada)
        {
            foreach (var item in ListaAsignada)
            {
                if (listaAprobados.Contains(item))
                {
                    listaAprobados.Remove(item);
                }
            }
            return listaAprobados;
        }
    }
}
