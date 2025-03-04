using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Interfaces;
using WPF_CHEAT_os.Models;
using WPF_CHEAT_os.Services;
using WPF_CHEAT_os.Utils;

namespace WPF_CHEAT_os.ViewModel
{
    public partial class VerPropuestaViewModel : ViewModelBase
    {
        private readonly IPropuestaProvider _propuestaProvider;
        private readonly IUsuarioProvider _usuarioService;

        [ObservableProperty]
        private ProfesorModel? profesor1;

        [ObservableProperty]
        private ProfesorModel? profesor2;

        [ObservableProperty]
        private ProfesorModel? profesor3;

        [ObservableProperty]
        private PropuestaDTO? propuesta;

        [ObservableProperty]
        private ObservableCollection<ProfesorModel> profesores = new();

        public VerPropuestaViewModel(IPropuestaProvider propuestaProvider, IUsuarioProvider usuarioService)
        {
            _propuestaProvider = propuestaProvider ?? throw new ArgumentNullException(nameof(propuestaProvider));
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
        }

        public async Task SetIdObjeto(int id)
        {
            await CargarPropuesta(id.ToString());
            await CargarProfesores();
            AsignarProfesores();
        }

        private void AsignarProfesores()
        {
            if (Propuesta?.UserIds != null && Propuesta.UserIds.Any())
            {
                var profesoresSeleccionados = Profesores.Where(p => Propuesta.UserIds.Contains(p.Id)).ToList();
                Profesor1 = profesoresSeleccionados.ElementAtOrDefault(0);
                Profesor2 = profesoresSeleccionados.ElementAtOrDefault(1);
                Profesor3 = profesoresSeleccionados.ElementAtOrDefault(2);
            }
            else
            {
                Profesor1 = null;
                Profesor2 = null;
                Profesor3 = null;
            }
        }

        [RelayCommand]
        private async Task Aceptar()
        {
            if (Propuesta == null) return;
            Propuesta.Estado = "Aceptada";
            Propuesta.UserIds ??= new List<string>();
            await GuardarYNotificar("La propuesta ha sido marcada como Aceptada.");
        }

        [RelayCommand]
        private async Task Rechazar()
        {
            if (Propuesta == null) return;
            Propuesta.Estado = "Rechazada";
            Propuesta.UserIds ??= new List<string>();
            await GuardarYNotificar("La propuesta ha sido marcada como Rechazada.");
        }

        [RelayCommand]
        private async Task RequerirAmpliacion()
        {
            if (Propuesta == null) return;
            Propuesta.Estado = "Requiere Ampliación";
            Propuesta.UserIds ??= new List<string>();
            await GuardarYNotificar("La propuesta ha sido marcada como Requiere Ampliación.");
        }

        [RelayCommand]
        private async Task Save()
        {
            if (Propuesta == null) return;

            // Obtener los profesores seleccionados
            var seleccionados = new[] { Profesor1, Profesor2, Profesor3 }
                                .Where(p => p != null)
                                .Distinct()
                                .ToList();

            // Verificar si hay duplicados
            if (seleccionados.Count < new[] { Profesor1, Profesor2, Profesor3 }.Count(p => p != null))
            {
                MessageBox.Show("No puedes seleccionar el mismo profesor más de una vez.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Actualizar UserIds con los IDs de los profesores seleccionados
            Propuesta.UserIds = seleccionados.Select(p => p!.Id).ToList();

            // Asegurar que la lista no es null
            Propuesta.UserIds ??= new List<string>();

            await GuardarYNotificar("Propuesta actualizada con éxito.");
        }


        private async Task GuardarYNotificar(string mensaje)
        {
            try
            {
                await _propuestaProvider.UpdateAsync(Propuesta);
                MessageBox.Show(mensaje, "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task CargarPropuesta(string id)
        {
            try
            {
                Propuesta = await _propuestaProvider.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la propuesta: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task CargarProfesores()
        {
            try
            {
                // Obtener los datos de los profesores
                var profesoresData = await _usuarioService.GetGetUsuarioDTOAsync() ?? new List<GetUsuarioDTO>();

                // Limpiar la lista actual de profesores
                Profesores.Clear();

                // Recorrer los profesores y agregarlos a la lista
                foreach (var profesor in profesoresData.Where(p => p != null && p.Rol == Constants.ROLE_REGISTRER_PROFESOR))
                {
                    // Validar si el ID es nulo o vacío
                    if (string.IsNullOrEmpty(profesor.Id))
                    {
                        MessageBox.Show($"Error: El ID del profesor '{profesor.Name}' es nulo o vacío.", "Error de ID", MessageBoxButton.OK, MessageBoxImage.Warning);
                        continue; // Saltar al siguiente profesor
                    }

                    // Agregar el profesor a la lista
                    Profesores.Add(new ProfesorModel { Id = profesor.Id, Nombre = profesor.Name });
                }
            }
            catch (Exception ex)
            {
                // Mostrar un mensaje de error si ocurre una excepción
                MessageBox.Show($"Error al cargar los profesores: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}