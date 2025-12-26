namespace CobblePedia.ViewModels
{
    using CobblePedia.Helpers;
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class EggGroupViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string eggIcon;

        private string translationKey;

        public EggGroupViewModel()
        {
            translationKey = string.Empty;
            name = string.Empty;
            eggIcon = string.Format(Properties.Resources.TypeIconPath, "empty"); ;
        }

        public EggGroupViewModel(EggGroup source)
        {
            translationKey = source.KeyName;
            eggIcon = string.Format(Properties.Resources.EggGroupPicture, source.EggGroupId);

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

