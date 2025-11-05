namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class NatureViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string decreasedStat;
        [ObservableProperty] private string hatesFlavor;
        [ObservableProperty] private string increasedStat;
        [ObservableProperty] private string likesFlavor;

        private Nature nature;

        public NatureViewModel(Nature source)
        {
            nature = source;
            name = "-";

            decreasedStat = nature.DecreasedStat;
            hatesFlavor = nature.HatesFlavor;
            increasedStat = nature.IncreasedStat;
            likesFlavor = nature.LikesFlavor;

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal void SetLanguage(string language)
        {
            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[nature.KeyName];
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[nature.KeyName];
                    break;
            }
        }
    }
}
