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

        private readonly VerPropuestaViewModel _viewModel;

        [ObservableProperty]
        private ViewModelBase? selectedViewModel;

        public PropuestaViewModel(IPropuestaProvider propuestaProvider, IServiceProvider serviceProvider,
                                    IUsuarioProvider usuarioProvider, IAsignarProvider asignarProvider, VerPropuestaViewModel viewModel)
        {
            _propuestaProvider = propuestaProvider;
            _serviceProvider = serviceProvider;
            _usuarioService = usuarioProvider;
            _asignarProvider = asignarProvider;
            _viewModel = viewModel;
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
                    var usersList = propuestaDto.Users?.ToList() ?? new List<ProfesorModel>();

                    var propuestaModel = new PropuestaModel
                    {
                        Id = propuestaDto.Id,
                        Titulo = propuestaDto.Titulo,
                        Email = propuestaDto.Email,
                        Descripcion = propuestaDto.Descripcion,
                        Estado = propuestaDto.Estado,
                        Profesor1 = usersList.Count > 0 ? new ProfesorModel { Nombre = usersList[0].Nombre } : new ProfesorModel(),
                        Profesor2 = usersList.Count > 1 ? new ProfesorModel { Nombre = usersList[1].Nombre } : new ProfesorModel(),
                        Profesor3 = usersList.Count > 2 ? new ProfesorModel { Nombre = usersList[2].Nombre } : new ProfesorModel()
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

            var propuestaService = new PropuestaService(new HttpsJsonClientService<PropuestaDTO>());
            var usuarioService = new UsuarioService(new HttpsJsonClientService<UsuarioDTO>(), new HttpsJsonClientService<GetUsuarioDTO>());

            var viewModel = new VerPropuestaViewModel(propuestaService, usuarioService);
            await viewModel.SetIdObjeto(SelectedPropuesta.Id);

            var view = new VerPropuestaView { DataContext = viewModel };
            view.ShowDialog();

            await LoadAsync();
        }
    }
}