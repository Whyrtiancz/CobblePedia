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

        private PokemonAbility pokemonAbility;
        private Ability ability;

        public AbilityViewModel(PokemonAbility source)
        {
            pokemonAbility = source;
            ability = CobblePedia.Pedia.Abilities[pokemonAbility.AbilityId];
            isHidden = pokemonAbility.IsHidden;
            visibleVisibility = Visibility.Visible;
            hiddenVisibility = Visibility.Collapsed;
            if (isHidden)
            {
                visibleVisibility = Visibility.Collapsed;
                hiddenVisibility = Visibility.Visible;
            }
            name = "-";


            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal void SetLanguage(string language)
        {
            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[ability.KeyName];
                    Effect = CobblePedia.Pedia.FR[ability.KeyEffect];
                    Flavor = CobblePedia.Pedia.FR[ability.KeyFlavor];
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[ability.KeyName];
                    Effect = CobblePedia.Pedia.EN[ability.KeyEffect];
                    Flavor = CobblePedia.Pedia.EN[ability.KeyFlavor];
                    break;
            }
        }
    }
}
