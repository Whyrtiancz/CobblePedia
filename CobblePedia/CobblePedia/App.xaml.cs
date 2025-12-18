// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CobblePedia
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.UI.Xaml;

    using Windows.Storage;

    using CobblePedia.Models;

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow Window => m_window;
        private static MainWindow m_window;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            var splash = new SplashWindow();
            splash.Activate();

            // Ici vous faites votre logique de démarrage (chargement config, DB, etc.)
            bool IsLoaded = await CobblePediaModel.Pedia.Load();
            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/JSON/types.json"));
            //string jsonContent = await FileIO.ReadTextAsync(file);

            m_window = new MainWindow();
            m_window.Activate();

            splash.Close();
        }
    }
}
