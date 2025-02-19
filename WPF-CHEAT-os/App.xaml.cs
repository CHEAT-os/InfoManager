using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF_CHEAT_os.ViewModel;
using WPF_CHEAT_os.Services;
using WPF_CHEAT_os.Interfaces;
using WPF_CHEAT_os.DTO;

namespace WPF_CHEAT_os;
public partial class App : Application
{
    public App()
    {
        Services = ConfigureServices();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = Current.Services.GetService<MainWindow>();
        mainWindow?.Show();
    }
    public new static App Current => (App)Application.Current;
    public IServiceProvider Services { get; }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        //view principal
        services.AddTransient<MainWindow>();

        //view viewModels
        services.AddTransient<MainViewModel>();
        services.AddTransient<PrincipalViewModel>();
        services.AddTransient<PropuestaViewModel>();
        services.AddTransient<VerPropuestaViewModel>();



        //Services
        services.AddSingleton<IPropuestaProvider<PropuestaDTO>, PropuestaService>();
    
        return services.BuildServiceProvider();
    }

   
}


