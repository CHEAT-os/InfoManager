using CommunityToolkit.Mvvm.ComponentModel;
using WPF_CHEAT_os.DTO;
using System.Collections.ObjectModel;
using System.Windows;
using WPF_CHEAT_os.Interfaces;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;



namespace WPF_CHEAT_os.ViewModel
{
    public partial class PropuestaViewModel : ViewModelBase
    {
        private readonly IPropuestaProvider<PropuestaDTO> _propuestaProvider;
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private ObservableCollection<PropuestaDTO> listaDePropuestas = new();

        [ObservableProperty]
        private PropuestaDTO selectedPropuesta;
   

        public PropuestaViewModel() { }
        public PropuestaViewModel(IPropuestaProvider<PropuestaDTO> propuestaProvider, IServiceProvider serviceProvider)
        {
            _propuestaProvider = propuestaProvider;
            _serviceProvider = serviceProvider;


            listaDePropuestas = new ObservableCollection<PropuestaDTO>();
        }


        public override async Task LoadAsync()
        {
            try
            {
                
                var propuestas = await _propuestaProvider.GetAsync();

                if (propuestas != null && propuestas.Count > 0)
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
            await mainViewModel.VerPropuestaViewModel.CargarPropuesta(SelectedPropuesta.Id);
            mainViewModel.SelectedViewModel = mainViewModel.VerPropuestaViewModel;
        }


    }


}


