using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Interfaces;
using WPF_CHEAT_os.Utils;

namespace WPF_CHEAT_os.ViewModel
{
    public partial class VerPropuestaViewModel : ViewModelBase
    {
        private readonly IPropuestaProvider<PropuestaDTO> _propuestaProvider;

        [ObservableProperty]
        private PropuestaDTO? propuesta;

        [ObservableProperty]
        private EstadoPropuesta estadoSeleccionado;

        [ObservableProperty]
        private PropuestaDTO? propuestaSeleccionada;

        public VerPropuestaViewModel(IPropuestaProvider<PropuestaDTO> propuestaProvider)
        {
            _propuestaProvider = propuestaProvider ?? throw new ArgumentNullException(nameof(propuestaProvider));
        }

        [RelayCommand]
        private void Aceptar()
        {
            if (Propuesta == null) return;

            EstadoSeleccionado = EstadoPropuesta.Aceptada;
            MessageBox.Show("La propuesta ha sido marcada como Aceptada.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        [RelayCommand]
        private void Rechazar()
        {
            if (Propuesta == null) return;

            EstadoSeleccionado = EstadoPropuesta.Rechazada;
            MessageBox.Show("La propuesta ha sido marcada como Rechazada.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        [RelayCommand]
        private async Task EnviarAsync()
        {
            if (PropuestaSeleccionada == null) return;

            try
            {
              

                PropuestaSeleccionada.Estado = EstadoSeleccionado;
                PropuestaSeleccionada.FechaEnvio = DateTime.Now;
                bool resultado = await _propuestaProvider.UpdateAsync(PropuestaSeleccionada);


                MessageBox.Show(resultado
                    ? "Propuesta actualizada correctamente en la base de datos."
                    : "Error al actualizar la propuesta.",
                    resultado ? "Éxito" : "Error",
                    MessageBoxButton.OK,
                    resultado ? MessageBoxImage.Information : MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Excepción", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        public async Task CargarPropuesta(int id)
        {
            try
            {

                PropuestaSeleccionada = await _propuestaProvider.GetByIdAsync(id);
                if (PropuestaSeleccionada != null)
                {
                    Propuesta = PropuestaSeleccionada;
                    EstadoSeleccionado = PropuestaSeleccionada.Estado;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la propuesta: {ex.Message}");
            }
        }

    }
}



