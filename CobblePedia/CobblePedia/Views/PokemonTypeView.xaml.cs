// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CobblePedia.Views
{
    using CobblePedia.ViewModels;

    using Microsoft.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PokemonTypeView : Page
    {
        internal CobblePediaViewModel ViewModel
        {
            get
            {
                return CobblePediaViewModel.Model;
            }
        }

        public PokemonTypeView()
        {
            InitializeComponent();
        }
    }
}
