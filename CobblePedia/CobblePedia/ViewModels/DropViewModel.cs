namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    public partial class DropViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string quantityRange;
        [ObservableProperty] private float percentage;
        [ObservableProperty] private string chance;
        [ObservableProperty] private string icon;

        private Drop drop;

        public DropViewModel(Drop source)
        {
            drop = source;
            name = source.DropId;
            quantityRange = source.QuantityRange;
            percentage = source.Percentage;
            chance = string.Format("{0:P1}", percentage / 100F);
            icon = string.Format(Properties.Resources.ItemPicture, name.Substring(name.IndexOf(':') + 1));

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal void SetLanguage(string language)
        {
            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[drop.DropId];
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[drop.DropId];
                    break;
            }
        }

    }
}
