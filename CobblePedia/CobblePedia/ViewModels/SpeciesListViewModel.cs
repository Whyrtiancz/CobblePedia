namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class SpeciesListViewModel : ObservableObject
    {
        [ObservableProperty] private int nationalPokedexNumber;
        [ObservableProperty] private string numero;
        [ObservableProperty] private string name;
        [ObservableProperty] private string description;
        [ObservableProperty] private string frontDefault;

        public GenerationViewModel Generation { get; private set; }
        public TypeViewModel PrimaryType { get; private set; }
        public TypeViewModel SecondaryType { get; private set; }

        private Pokemon species;

        public SpeciesListViewModel(Pokemon source)
        {
            species = source;

            nationalPokedexNumber = species.NationalPokedexNumber;
            numero = "#" + nationalPokedexNumber.ToString("D4");
            name = "-";
            description = "-";

            Generation = new GenerationViewModel(CobblePedia.Pedia.Generations[species.Generation]);

            PrimaryType = new TypeViewModel(CobblePedia.Pedia.Types[species.PrimaryType]);
            if (species.SecondaryType == null)
            {
                SecondaryType = new TypeViewModel();
            }
            else
            {
                SecondaryType = new TypeViewModel(CobblePedia.Pedia.Types[species.SecondaryType]);
            }

            frontDefault = string.Format(Properties.Resources.PokemonPicturePath, species.Picture.FrontDefault);


            SetLanguage("en");
        }

        internal void SetLanguage(string language)
        {
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
