namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class PokemonSimplifiedViewModel : ObservableObject
    {
        [ObservableProperty] private bool isImplemented;
        [ObservableProperty] private bool isForm;
        [ObservableProperty] private int nationalPokedexNumber;
        [ObservableProperty] private string numero;
        [ObservableProperty] private string name;
        [ObservableProperty] private string description;
        [ObservableProperty] private string frontDefault;

        public GenerationViewModel Generation { get; private set; }
        public TypeSimplifiedViewModel PrimaryType { get; private set; }
        public TypeSimplifiedViewModel SecondaryType { get; private set; }

        internal Pokemon species;

        public PokemonSimplifiedViewModel()
        {
            isImplemented = false;
            isForm = false;
            nationalPokedexNumber = 0;
            numero = string.Empty;
            name = string.Empty;
            description = string.Empty;
            frontDefault = string.Format(Properties.Resources.PokemonPicturePath, "0_none_front_default.png");
        }

        public PokemonSimplifiedViewModel(Pokemon source)
        {
            species = source;
            isImplemented = species.Implemented;
            isForm = species.IsForm;

            nationalPokedexNumber = species.NationalPokedexNumber;
            numero = "#" + nationalPokedexNumber.ToString("D4");
            name = "-";
            description = "-";

            Generation = new GenerationViewModel(CobblePedia.Pedia.Generations[species.Generation]);

            PrimaryType = new TypeSimplifiedViewModel(CobblePedia.Pedia.Types[species.PrimaryType]);
            if (species.SecondaryType == null)
            {
                SecondaryType = new TypeSimplifiedViewModel();
            }
            else
            {
                SecondaryType = new TypeSimplifiedViewModel(CobblePedia.Pedia.Types[species.SecondaryType]);
            }

            frontDefault = string.Format(Properties.Resources.PokemonPicturePath, species.Picture.FrontDefault);

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }
        internal PokemonExtendedViewModel GetExtendedViewModel()
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedPokemon"] = this.species.SpeciesId;
            return new PokemonExtendedViewModel(this.species);
        }

        internal void SetLanguage(string language)
        {
            if (species == null)
                return;

            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[species.KeyName];
                    Description = CobblePedia.Pedia.FR[species.KeyDescription];
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[species.KeyName];
                    Description = CobblePedia.Pedia.EN[species.KeyDescription];
                    break;
            }

            Generation.SetLanguage(language);
            PrimaryType.SetLanguage(language);
            SecondaryType.SetLanguage(language);
        }
    }
}
