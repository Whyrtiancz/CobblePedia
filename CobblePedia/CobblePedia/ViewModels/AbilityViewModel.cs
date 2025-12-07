namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    using Microsoft.UI.Xaml;

    public partial class AbilityViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string effect;
        [ObservableProperty] private string flavor;
        [ObservableProperty] private bool isHidden;
        [ObservableProperty] private Visibility visibleVisibility;
        [ObservableProperty] private Visibility hiddenVisibility;

        private string translationKey;
        private string translationEffectKey;
        private string translationFlavorKey;

        public AbilityViewModel(PokemonAbility source)
        {
            Ability ability = CobblePedia.Pedia.Abilities[source.AbilityId];

            translationKey = ability.KeyName;
            translationEffectKey = ability.KeyEffect;
            translationFlavorKey = ability.KeyFlavor;

            isHidden = source.IsHidden;
            visibleVisibility = Visibility.Visible;
            hiddenVisibility = Visibility.Collapsed;
            if (isHidden)
            {
                visibleVisibility = Visibility.Collapsed;
                hiddenVisibility = Visibility.Visible;
            }
            name = string.Empty;
            effect = string.Empty;
            flavor = string.Empty;

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal void SetLanguage(string language)
        {
            Name = CobblePedia.Pedia.Translations[language][translationKey];
            Effect = CobblePedia.Pedia.Translations[language][translationEffectKey];
            Flavor = CobblePedia.Pedia.Translations[language][translationFlavorKey];
        }
    }
}
