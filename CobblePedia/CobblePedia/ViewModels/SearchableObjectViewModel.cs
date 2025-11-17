namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class SearchableObjectViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string icon;
        [ObservableProperty] private string label;
        [ObservableProperty] private string searchLabel;

        public string KeyType { get; private set; }
        public object Source { get; private set; }

        private string searchValues;
        private string keyName;
        private string objectId;

        public SearchableObjectViewModel(Pokemon pokemon)
        {
            Source = pokemon;
            objectId = pokemon.SpeciesId;
            keyName = pokemon.KeyName;
            KeyType = "@pokemon";
            label = "#" + pokemon.NationalPokedexNumber.ToString("D4");
            icon = string.Format(Properties.Resources.PokemonPicturePath, pokemon.Picture.FrontDefault);

            searchValues = string.Join("|", objectId, pokemon.NationalPokedexNumber.ToString("D4"), CobblePedia.Pedia.FR[keyName]).ToLower();

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        public SearchableObjectViewModel(PokemonType pokemonType)
        {
            Source = pokemonType;
            objectId = pokemonType.TypeId;
            keyName = pokemonType.KeyName;
            KeyType = "@type";
            label = string.Empty;
            icon = string.Format(Properties.Resources.TypeIconPath, pokemonType.TypeId);

            searchValues = string.Join("|", objectId, CobblePedia.Pedia.FR[keyName]).ToLower();

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        public SearchableObjectViewModel(Trainer trainer)
        {
            Source = trainer;
            objectId = trainer.TrainerId;
            keyName = trainer.KeyName;
            KeyType = "@trainer";
            label = string.Join(",", trainer.Series);
            icon = "Assets/Trainers/default.png";

            searchValues = string.Join("|", objectId, CobblePedia.Pedia.FR[keyName]).ToLower();

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        public bool IsElligible(string searchPattern)
        {
            bool isElligible = false;

            var querySplit = searchPattern.ToLower().Split(" ");
            foreach (string item in querySplit)
            {
                isElligible = isElligible || searchValues.Contains(item);
                if (isElligible)
                {
                    return isElligible;
                }
            }

            return isElligible;
        }

        internal void SetLanguage(string language)
        {
            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[keyName];
                    SearchLabel = string.Format("{0}: {1}", KeyType, Name);
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[keyName];
                    SearchLabel = string.Format("{0}: {1}", KeyType, Name);
                    break;
            }
        }
        internal ObservableObject GetExtendedViewModel()
        {
            ObservableObject result = null;

            switch (KeyType)
            {
                case "@type":
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedType"] = objectId;
                    result = new TypeExtendedViewModel(Source as PokemonType, null, false);
                    break;
                case "@pokemon":
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedPokemon"] = objectId;
                    result = new PokemonExtendedViewModel(Source as Pokemon);
                    break;
                case "@trainer":
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values["Selectedtrainer"] = objectId;
                    result = new TrainerExtendedViewModel(Source as Trainer);
                    break;
            }

            return result;
        }


        public override string ToString()
        {
            return SearchLabel;
        }
    }
}
