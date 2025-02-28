using CommunityToolkit.Mvvm.ComponentModel;
using WPF_CHEAT_os.DTO;
using System.Collections.ObjectModel;
using System.Windows;
using WPF_CHEAT_os.Interfaces;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace WPF_CHEAT_os.ViewModel
{
    public partial class PropuestaViewModel : ViewModelBase
    {
        private readonly IPropuestaProvider _propuestaProvider;
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private ObservableCollection<PropuestaDTO> listaDePropuestas = new();

        [ObservableProperty]
        private PropuestaDTO selectedPropuesta;

        public PropuestaViewModel(IPropuestaProvider propuestaProvider, IServiceProvider serviceProvider)
        {
            _propuestaProvider = propuestaProvider;
            _serviceProvider = serviceProvider;
        }

        public override async Task LoadAsync()
        {
            try
            {
                var propuestas = await _propuestaProvider.GetAsync();

                if (propuestas != null)
                {
                    ListaDePropuestas.Clear();
                    foreach (var propuesta in propuestas)
                    {
                        ListaDePropuestas.Add(propuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las propuestas: {ex.Message}");
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
    }
}
