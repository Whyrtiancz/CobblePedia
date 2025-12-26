// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CobblePedia.Views
{
    using CobblePedia.ViewModels;

    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TrainerView : Page
    {
        internal CobblePediaViewModel ViewModel => CobblePediaViewModel.Model;

        public TrainerView()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (ViewModel.PokemonViewModels.Count == 0)
            {
                await ViewModel.LoadTrainerAsync();
            }
        }
    }
}
