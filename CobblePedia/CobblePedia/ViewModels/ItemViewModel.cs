namespace CobblePedia.ViewModels
{
    using CobblePedia.Helpers;
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    public partial class ItemViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string icon;

        private string translationKey;

        public ItemViewModel()
        {
            translationKey = string.Empty;
            name = string.Empty;
            icon = string.Format(Properties.Resources.ItemPicture, "none");
        }

        public ItemViewModel(string item)
        {
            translationKey = item;
            name = item;
            icon = string.Format(Properties.Resources.ItemPicture, item.Substring(item.IndexOf(':') + 1));

            SetLanguage();
        }

        internal void SetLanguage()
        {
            if (translationKey == string.Empty)
                return;

            Name = CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()][translationKey];
        }
    }
}
