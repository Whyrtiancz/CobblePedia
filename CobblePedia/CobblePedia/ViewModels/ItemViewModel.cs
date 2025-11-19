namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    public partial class ItemViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string icon;

        private string itemKey;

        public ItemViewModel()
        {
            itemKey = string.Empty;
            icon = string.Format(Properties.Resources.ItemPicture, "none");
        }

        public ItemViewModel(string item)
        {
            itemKey = item;
            icon = string.Format(Properties.Resources.ItemPicture, item.Substring(item.IndexOf(':') + 1));

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal void SetLanguage(string language)
        {
            if (itemKey == string.Empty)
                return;

            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[itemKey];
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[itemKey];
                    break;
            }
        }
    }
}
