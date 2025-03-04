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
using WPF_CHEAT_os.Models;
using WPF_CHEAT_os.Services;
using WPF_CHEAT_os.View;

namespace WPF_CHEAT_os.ViewModel
{
    public partial class PropuestaViewModel : ViewModelBase
    {
        private readonly IPropuestaProvider _propuestaProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUsuarioProvider _usuarioService;
        private readonly IAsignarProvider _asignarProvider;

        [ObservableProperty]
        private ObservableCollection<PropuestaModel> listaDePropuestas = new();

        [ObservableProperty]
        private ObservableCollection<ProfesorModel> profesores = new();

        [ObservableProperty]
        private PropuestaModel? selectedPropuesta;

        public PropuestaViewModel(IPropuestaProvider propuestaProvider, IServiceProvider serviceProvider,
                                    IUsuarioProvider usuarioProvider, IAsignarProvider asignarProvider)
        {
            _propuestaProvider = propuestaProvider;
            _serviceProvider = serviceProvider;
            _usuarioService = usuarioProvider;
            _asignarProvider = asignarProvider;
        }

        public override async Task LoadAsync()
        {
            try
            {
                var propuestas = await _propuestaProvider.GetAsync();
                var profesoresData = await _usuarioService.GetGetUsuarioDTOAsync();
                ListaDePropuestas.Clear();
                foreach (var propuestaDto in propuestas)
                {
                    var userIds = propuestaDto.UserIds?.ToList() ?? new List<string>();

                    var propuestaModel = new PropuestaModel
                    {
                        Id = propuestaDto.Id,
                        Titulo = propuestaDto.Titulo,
                        Email = propuestaDto.Email,
                        Descripcion = propuestaDto.Descripcion,
                        Estado = propuestaDto.Estado,
                        Profesor1 = userIds.Count > 0 ? userIds[0] : null, 
                        Profesor2 = userIds.Count > 1 ? userIds[1] : null, 
                        Profesor3 = userIds.Count > 2 ? userIds[2] : null  
                    };

                    ListaDePropuestas.Add(propuestaModel);
                }

                Profesores.Clear();
                foreach (var profesor in profesoresData)
                {
                    if (profesor.Rol.Equals(Constants.ROLE_REGISTRER_PROFESOR))
                    {
                        Profesores.Add(new ProfesorModel { Nombre = profesor.Name });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task VerView()
        {
            if (SelectedPropuesta == null) return;

            var viewModel = _serviceProvider.GetRequiredService<VerPropuestaViewModel>();
            await viewModel.SetIdObjeto(SelectedPropuesta.Id);

            var view = new VerPropuestaView { DataContext = viewModel };
            view.ShowDialog();

            await LoadAsync();
        }

        [RelayCommand]
        private async Task Autocompletar()
        {
            try
            {
                var usuarios = await _usuarioService.GetUsuarioDTOAsync();

                if (usuarios == null || !usuarios.Any())
                {
                    MessageBox.Show("No se encontraron usuarios.");
                    return;
                }

                int? cursoId = usuarios
                    .SelectMany(u => u.Cursos)
                    .Where(c => SelectedPropuesta.UserIds.Any(userId => usuarios.Any(u => u.Id.Equals(userId) && u.Cursos.Any(uc => uc.Id == c.Id))))
                    .Select(c => c.Id)
                    .FirstOrDefault();

                if (cursoId == null || cursoId == 0)
                {
                    MessageBox.Show("No se encontró el curso para esta propuesta.");
                    return;
                }

                var alumnos = usuarios
                    .Where(u => u.Rol.Equals(Constants.ROLE_REGISTRER_ALUMNO, StringComparison.OrdinalIgnoreCase) &&
                                u.Cursos.Count == 1 &&
                                u.Cursos.First().Id == cursoId)
                    .ToList();

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

                int maxAlumnosPorProfesor = _asignarProvider.TutorPuedeLlevar(108, alumnos.Count);
                var listaAsignada = _asignarProvider.AlumnosAsignados(alumnos, maxAlumnosPorProfesor);
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