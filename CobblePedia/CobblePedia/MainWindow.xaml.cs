// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CobblePedia
{
    using System;
    using System.Linq;

    using CobblePedia.ViewModels;
    using CobblePedia.Views;

    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.Windows.Storage;

    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        internal void SetLanguage(string language)
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = language;
            if (navFrame.IsNavigationStackEnabled)
                return;

            navFrame.Navigate(typeof(Settings));
            navFrame.BackStack.Clear();
        }

        public MainWindow()
        {
            InitializeComponent();

            ApplicationDataContainer localSettings = ApplicationData.GetDefault().LocalSettings;
            int? themeIndex = Windows.Storage.ApplicationData.Current.LocalSettings.Values["Theme"] as int?;
            if (themeIndex is 0 or 1)
            {
                this.Root.RequestedTheme = (ElementTheme)themeIndex.Value;
            }
            string? applicationLanguage = Windows.Storage.ApplicationData.Current.LocalSettings.Values["ApplicationLanguage"] as string;
            if (applicationLanguage is "en-US" or "fr-FR")
            {
                Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = applicationLanguage;
            }
            string? dataLanguage = Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"] as string;
            switch (dataLanguage)
            {
                case "fr":
                    CobblePediaViewModel.Model.SetFR();
                    break;
                default:
                    CobblePediaViewModel.Model.SetEN();
                    break;
            }

            this.ExtendsContentIntoTitleBar = true;
            this.AppWindow.TitleBar.PreferredHeightOption = Microsoft.UI.Windowing.TitleBarHeightOption.Tall;
            this.SetTitleBar(titleBar); // Set the custom title bar
            navView.SelectedItem = navView.MenuItems.OfType<NavigationViewItem>().First();

            if (Content is FrameworkElement root)
                root.DataContext = CobblePediaViewModel.Model;
        }

        private void TitleBar_PaneToggleRequested(TitleBar sender, object args)
        {
            navView.IsPaneOpen = !navView.IsPaneOpen;
        }

        private void TitleBar_BackRequested(TitleBar sender, object args)
        {
            navFrame.GoBack();
        }

        private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = (NavigationViewItem)args.SelectedItem;
            if (selectedItem != null)
            {
                string selectedItemTag = ((string)selectedItem.Tag);
                //sender.Header = "Sample Page " + selectedItemTag.Substring(selectedItemTag.Length - 1);
                string pageName = "CobblePedia.Views." + selectedItemTag;
                Type pageType = Type.GetType(pageName);
                navFrame.Navigate(pageType);
            }
        }
    }
}
