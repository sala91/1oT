using System;
using _1oT.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Windows.Storage;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace _1oT
{
    public sealed partial class App : Application
    {
        private Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();

            // Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            // Create IOC container and add logging feature to it.
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();

            // Build provider to access the logging service.
            IServiceProvider provider = services.BuildServiceProvider();

            // UWP is very restrictive of where you can save files on the disk.
            // The preferred place to do that is app's local folder.
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            string fullPath = $"{folder.Path}\\Logs\\App.log";

            // Tell the logging service to use Serilog.File extension.
            provider.GetService<ILoggerFactory>().AddFile(fullPath);

            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.MainPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }

        protected override async void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }
    }
}
