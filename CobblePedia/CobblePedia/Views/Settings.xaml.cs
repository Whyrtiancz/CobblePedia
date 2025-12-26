using CobblePedia.Helpers;
using CobblePedia.ViewModels;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

using Newtonsoft.Json.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CobblePedia.Views;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Settings : Page
{
    internal CobblePediaViewModel ViewModel
    {
        get
        {
            return CobblePediaViewModel.Model;
        }
    }

    public Settings()
    {
        InitializeComponent();

        ApplicationLanguageComboBox.SelectedValue = SettingsHelper.GetApplicationLanguage();
        DataLanguageComboBox.SelectedValue = SettingsHelper.GetDataLanguage();
        ThemeComboBox.SelectedIndex = SettingsHelper.GetTheme();
    }

    private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ElementTheme theme = ElementTheme.Default;
        switch (((ComboBox)sender).SelectedIndex)
        {
            case 1:
                theme = ElementTheme.Light;
                break;
            case 2:
                theme = ElementTheme.Dark;
                break;
        }
        FrameworkElement root = this.XamlRoot?.Content as FrameworkElement;
        if (root != null)
        {
            root.RequestedTheme = theme;
            SettingsHelper.SetTheme((int)theme);
        }
    }

    private void ApplicationLanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string language = "en-US";
        switch (((ComboBox)sender).SelectedIndex)
        {
            case 0:
                language = "fr-FR";
                break;
            case 1:
                language = "en-US";
                break;
        }
        App.Window.SetLanguage(language);
        SettingsHelper.SetApplicationLanguage(language);
    }

    private void DataLanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string language = "en";
        switch (((ComboBox)sender).SelectedIndex)
        {
            case 0:
                SettingsHelper.SetDataLanguage("en");
                break;
            case 1:
                SettingsHelper.SetDataLanguage("fr");
                break;
        }
    }
}
