// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CobblePedia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CobblePedia.ViewModels;
    using CobblePedia.Views;

    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Media.Imaging;
    using Microsoft.Windows.Storage;

    using Windows.System;

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

            CobblePediaViewModel.Model.Initialize();

            if (Content is FrameworkElement root)
            {
                root.DataContext = CobblePediaViewModel.Model;

                root.Loaded += Root_Loaded;
            }
        }

        #region Events
        private async void Root_Loaded(object sender, RoutedEventArgs e)
        {
            await PopulateCurrentUserAsync();
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
                string pageName = "CobblePedia.Views." + selectedItemTag;
                Type pageType = Type.GetType(pageName);
                navFrame.Navigate(pageType);
            }
        }

        private void OnControlsSearchBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                List<SearchableObjectViewModel> suggestions = CobblePediaViewModel.Model.Filter(sender.Text);
                if (suggestions.Count > 0)
                {
                    //controlsSearchBox.ItemsSource = suggestions.OrderByDescending(i => i.ElligibleValue).Select(item => item.ElligibleValue).ToList();
                    controlsSearchBox.ItemsSource = suggestions.ToList();
                }
                else
                {
                    controlsSearchBox.ItemsSource = new string[] { "No results found" };
                }
            }
        }

        private void OnControlsSearchBoxQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null && args.ChosenSuggestion is SearchableObjectViewModel)
            {
                SearchableObjectViewModel infoDataItem = args.ChosenSuggestion as SearchableObjectViewModel;
                if (infoDataItem != null && infoDataItem.ViewType != string.Empty)
                {
                    switch (infoDataItem.KeyType)
                    {
                        case "@type":
                            CobblePediaViewModel.Model.SelectedTypeInList = infoDataItem;
                            break;
                        case "@pokemon":
                            CobblePediaViewModel.Model.SelectedPokemonInList = infoDataItem;
                            break;
                        case "@trainer":
                            CobblePediaViewModel.Model.SelectedTrainerInList = infoDataItem;
                            break;
                        default:
                            break;
                    }
                    Type pageType = Type.GetType(infoDataItem.ViewType);
                    if (pageType != null && navFrame.Content.GetType() != pageType)
                    {
                        navFrame.Navigate(pageType);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(args.QueryText))
            {
                //Navigate(typeof(SearchResultsPage), args.QueryText);
            }
        }

        private void KeyboardAccelerator_Invoked(Microsoft.UI.Xaml.Input.KeyboardAccelerator sender, Microsoft.UI.Xaml.Input.KeyboardAcceleratorInvokedEventArgs args)
        {

        }
        #endregion Events

        private async Task PopulateCurrentUserAsync()
        {
            var users = await User.FindAllAsync();
            var user = users.FirstOrDefault(u =>
                            u.AuthenticationStatus == UserAuthenticationStatus.LocallyAuthenticated &&
                            u.Type == UserType.LocalUser)
                       ?? users.FirstOrDefault();

            if (user is null) return;

            string displayName = (string)await user.GetPropertyAsync(KnownUserProperties.DisplayName);
            string first = (string)await user.GetPropertyAsync(KnownUserProperties.FirstName);
            string last = (string)await user.GetPropertyAsync(KnownUserProperties.LastName);

            // Calcul d’initiales simple F L -> "FL"
            string initials = string.Concat(new[] { first, last }
                                .Where(s => !string.IsNullOrWhiteSpace(s))
                                .Select(s => s.Trim()[0]))
                                .ToUpperInvariant();

            CurrentUser.DisplayName = string.IsNullOrWhiteSpace(displayName)
                                      ? $"{first} {last}".Trim()
                                      : displayName;
            CurrentUser.Initials = initials;

            var picRef = await user.GetPictureAsync(UserPictureSize.Size64x64);
            if (picRef != null)
            {
                using var ras = await picRef.OpenReadAsync();
                var bmp = new BitmapImage();
                await bmp.SetSourceAsync(ras);
                CurrentUser.ProfilePicture = bmp; // PersonPicture affiche la photo si présente
            }
        }

        //public void EnsureNavigationSelection(string id)
        //{
        //    foreach (object rawGroup in this.NavigationView.MenuItems)
        //    {
        //        if (rawGroup is NavigationViewItem group)
        //        {
        //            foreach (object rawItem in group.MenuItems)
        //            {
        //                if (rawItem is NavigationViewItem item)
        //                {
        //                    if ((string)item.Tag == id)
        //                    {
        //                        group.IsExpanded = true;
        //                        NavigationView.SelectedItem = item;
        //                        item.IsSelected = true;
        //                        return;
        //                    }
        //                    else if (item.MenuItems.Count > 0)
        //                    {
        //                        foreach (var rawInnerItem in item.MenuItems)
        //                        {
        //                            if (rawInnerItem is NavigationViewItem innerItem)
        //                            {
        //                                if ((string)innerItem.Tag == id)
        //                                {
        //                                    group.IsExpanded = true;
        //                                    item.IsExpanded = true;
        //                                    NavigationView.SelectedItem = innerItem;
        //                                    innerItem.IsSelected = true;
        //                                    return;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
