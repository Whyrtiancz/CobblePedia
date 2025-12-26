namespace CobblePedia.ViewModels
{
    using CobblePedia.Helpers;
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
            SetLanguage();
        }

        internal void SetLanguage()
        {
            Name = CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()][translationKey];
        }
    }
}
