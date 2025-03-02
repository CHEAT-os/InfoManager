using CommunityToolkit.Mvvm.ComponentModel;
using WPF_CHEAT_os.DTO;
using System.Collections.ObjectModel;
using System.Windows;
using WPF_CHEAT_os.Interfaces;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WPF_CHEAT_os.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WPF_CHEAT_os.Models;

namespace WPF_CHEAT_os.ViewModel
{
    public partial class PropuestaViewModel : ViewModelBase
    {
        private readonly IPropuestaProvider _propuestaProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUsuarioProvider _usuarioService;
        private readonly IAsignarProvider _asignarProvider;

        [ObservableProperty]
        private ObservableCollection<PropuestaModel> listaDePropuestas;

        [ObservableProperty]
        private ObservableCollection<UsuarioDTO> _Profesores;

        [ObservableProperty]
        private PropuestaDTO selectedPropuesta;

        [ObservableProperty]
        private string _Nombre;

        [ObservableProperty]
        private string _Descripcion;

        [ObservableProperty]
        private string _Estado;

        public PropuestaViewModel(IPropuestaProvider propuestaProvider, IServiceProvider serviceProvider, 
                                    IUsuarioProvider usuarioProvider, IAsignarProvider asignarProvider)
        {
            _propuestaProvider = propuestaProvider;
            _serviceProvider = serviceProvider;
            _usuarioService = usuarioProvider;
            _asignarProvider = asignarProvider;

            listaDePropuestas = new ObservableCollection<PropuestaModel>();
            Profesores = new ObservableCollection<UsuarioDTO>();
        }

        public override async Task LoadAsync()
        {
            try
            {
                var propuestasDto = await _propuestaProvider.GetAsync();
                var profesores = await _usuarioService.GetAsync();

                if (propuestasDto != null)
                {
                    ListaDePropuestas.Clear();
                    foreach (var propuestaDto in propuestasDto)
                    {
                        var propuestaModel = new PropuestaModel
                        {
                            Titulo = propuestaDto.Titulo,
                            Email = propuestaDto.Email,
                            Descripcion = propuestaDto.Descripcion,
                            Estado = propuestaDto.Estado,
                            Profesor1 = string.Empty,
                            Profesor2 = string.Empty,
                            Profesor3 = string.Empty
                        };

                        ListaDePropuestas.Add(propuestaModel);
                    }
                }

                if (profesores != null)
                {
                    Profesores.Clear();
                    foreach (var profesor in profesores)
                    {
                        if (profesor.Rol.Equals(Constants.ROLE_REGISTRER_PROFESOR))
                        {
                            Profesores.Add(profesor);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las propuestas o los profesores: {ex.Message}");
            }
        }


        [RelayCommand]
        private async Task VerDetallesAsync(PropuestaDTO propuesta)
        {
            if (propuesta == null) return;

            SelectedPropuesta = propuesta;

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            await mainViewModel.VerPropuestaViewModel.CargarPropuesta(SelectedPropuesta.Id.ToString());
            mainViewModel.SelectedViewModel = mainViewModel.VerPropuestaViewModel;
        }

        [RelayCommand]
        private async Task Autocompletar()
        {
            try
            {
                // Obtener todos los usuarios
                var usuarios = await _usuarioService.GetAsync();

                if (usuarios == null || !usuarios.Any())
                {
                    MessageBox.Show("No se encontraron usuarios.");
                    return;
                }

                // Obtener el ID del curso desde los usuarios relacionados con la propuesta
                int? cursoId = usuarios
                    .SelectMany(u => u.Cursos)
                    .Where(c => SelectedPropuesta.Users.Any(user => user.Cursos.Any(uc => uc.Id == c.Id)))
                    .Select(c => c.Id)
                    .FirstOrDefault();

                if (cursoId == null || cursoId == 0)
                {
                    MessageBox.Show("No se encontró el curso para esta propuesta.");
                    return;
                }

                // Filtrar alumnos (solo tienen un curso)
                var alumnos = usuarios
                    .Where(u => u.Rol.Equals(Constants.ROLE_REGISTRER_ALUMNO, StringComparison.OrdinalIgnoreCase) &&
                                u.Cursos.Count == 1 &&
                                u.Cursos.First().Id == cursoId)
                    .ToList();

                // Filtrar profesores (pueden tener varios cursos)
                var profesores = usuarios
                    .Where(u => u.Rol.Equals(Constants.ROLE_REGISTRER_PROFESOR, StringComparison.OrdinalIgnoreCase) &&
                                u.Cursos.Any(c => c.Id == cursoId))
                    .ToList();

                if (!alumnos.Any())
                {
                    MessageBox.Show("No se encontraron alumnos para este curso.");
                    return;
                }

                if (!profesores.Any())
                {
                    MessageBox.Show("No se encontraron profesores para este curso.");
                    return;
                }

                // Determinar cuántos alumnos puede llevar cada profesor
                int maxAlumnosPorProfesor = _asignarProvider.TutorPuedeLlevar(108, alumnos.Count); 
                // 540 / 5 = 108, no tenemos como saber hora de cada profe, se al principio de forma equitativa

                // Asignar alumnos de forma aleatoria
                var listaAsignada = _asignarProvider.AlumnosAsignados(alumnos, maxAlumnosPorProfesor);

                // Limpiar la lista de alumnos aprobados
                var alumnosRestantes = _asignarProvider.ListaClean(alumnos, listaAsignada);

                MessageBox.Show($"Asignación completada: {listaAsignada.Count} alumnos asignados.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al autocompletar: {ex.Message}");
            }
        }
    }
}
