using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows;

namespace WPF_CHEAT_os.ViewModel
{
    // commit
    public partial class MainViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;

        public MainViewModel() { }
        public MainViewModel(PrincipalViewModel principalViewModel, PropuestaViewModel propuestaViewModel)
        {
            _selectedViewModel = principalViewModel;  // Inicia con PrincipalView
            PrincipalViewModel = principalViewModel;
            PropuestaViewModel = propuestaViewModel;

        }

        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }

        public PropuestaViewModel  PropuestaViewModel { get; }
    public PrincipalViewModel  PrincipalViewModel { get; }
        public async override Task LoadAsync()
        {
            if (SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }

        [RelayCommand]
        private async Task SelectViewModelAsync(ViewModelBase viewModelParameter)
        {
         
            if (viewModelParameter is not ViewModelBase viewModel)
            {
                return;
            }

            SelectedViewModel = viewModel;

            try
            {
                await SelectedViewModel.LoadAsync();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al cargar el ViewModel: {ex.Message}");
            }
        }


    }
}
