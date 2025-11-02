using CobblePedia.ViewModels;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

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

        int? value = Windows.Storage.ApplicationData.Current.LocalSettings.Values["ApplicationLanguageIndex"] as int?;
        if (value is 0 or 1)
        {
            ApplicationLanguageComboBox.SelectedIndex = (int)value;
        }
        else
        {
            ApplicationLanguageComboBox.SelectedIndex = 0;
        }
        value = Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguageIndex"] as int?;
        if (value is 0 or 1)
        {
            DataLanguageComboBox.SelectedIndex = (int)value;
        }
        else
        {
            DataLanguageComboBox.SelectedIndex = 0;
        }
        value = Windows.Storage.ApplicationData.Current.LocalSettings.Values["themeIndex"] as int?;
        if (value is 0 or 1)
        {
            ThemeComboBox.SelectedIndex = (int)value;
        }
        else
        {
            ThemeComboBox.SelectedIndex = 0;
        }
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
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["Theme"] = ((int)theme);
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["themeIndex"] = ((ComboBox)sender).SelectedIndex;
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
        Windows.Storage.ApplicationData.Current.LocalSettings.Values["ApplicationLanguage"] = language;
        Windows.Storage.ApplicationData.Current.LocalSettings.Values["ApplicationLanguageIndex"] = ((ComboBox)sender).SelectedIndex;
    }

    private void DataLanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string language = "en";
        switch (((ComboBox)sender).SelectedIndex)
        {
            case 0:
                CobblePediaViewModel.Model.SetFR();
                language = "fr";
                break;
            case 1:
                CobblePediaViewModel.Model.SetEN();
                break;
        }
        Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"] = language;
        Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguageIndex"] = ((ComboBox)sender).SelectedIndex;
    }
}
