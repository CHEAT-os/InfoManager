using System.Windows;
using WPF_CHEAT_os.ViewModel;

namespace WPF_CHEAT_os
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        //De Normal no ponemos NUNCA async void, es siempre Task,
        //es necesario en este caso por respetar la signatura de Loaded
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await _viewModel.LoadAsync();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Error al cargar los datos: {ex.Message}");
            }
        }
    }
}