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

        private string translationKey;

        public NatureViewModel(Nature source)
        {
            translationKey = source.KeyName;
            name = "-";

            decreasedStat = source.DecreasedStat;
            hatesFlavor = source.HatesFlavor;
            increasedStat = source.IncreasedStat;
            likesFlavor = source.LikesFlavor;

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal void SetLanguage(string language)
        {
            Name = CobblePedia.Pedia.Translations[language][translationKey];
        }
    }
}
