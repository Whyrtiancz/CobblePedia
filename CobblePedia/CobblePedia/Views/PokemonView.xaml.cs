using CobblePedia.ViewModels;

using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CobblePedia.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class PokemonView : Page
{
    internal CobblePediaViewModel ViewModel
    {
        get
        {
            return CobblePediaViewModel.Model;
        }
    }

    public PokemonView()
    {
        InitializeComponent();

        PokemonListview.SelectionChanged += PokemonListview_SelectionChanged;
        //this.DataContext = ViewModel;
    }

    private void PokemonListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ViewModel.SelectedPokemonInList != null & PokemonListview.Items.Count > 0)
        {
            PokemonListview.ScrollIntoView(ViewModel.SelectedPokemonInList, ScrollIntoViewAlignment.Leading);
        }
    }
}
