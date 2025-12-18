namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class GenerationViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string region;

        private string translationKey;

        public GenerationViewModel(Generation source)
        {
            translationKey = source.KeyName;
            region = source.Region;
            name = "-";
            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal void SetLanguage(string language)
        {
            Name = CobblePediaModel.Pedia.Translations[language][translationKey];
        }
    }
}
