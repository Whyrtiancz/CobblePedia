namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    public partial class SearchableObjectViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string icon;
        [ObservableProperty] private string label;
        [ObservableProperty] private string searchLabel;

        public string ObjectId { get; private set; }
        public string KeyType { get; private set; }
        public object Source { get; private set; }

        private string searchValues;
        private string keyName;

        public SearchableObjectViewModel()
        {
            Source = null;
            name = string.Empty;
            ObjectId = string.Empty;
            keyName = string.Empty;
            KeyType = string.Empty;
            label = string.Empty;
            icon = string.Format(Properties.Resources.TypeIconPath, "empty"); ;

            searchValues = string.Empty;
        }

        public SearchableObjectViewModel(Pokemon pokemon)
        {
            Source = pokemon;
            ObjectId = pokemon.SpeciesId;
            keyName = pokemon.KeyName;
            KeyType = "@pokemon";
            label = "#" + pokemon.NationalPokedexNumber.ToString("D4");
            icon = string.Format(Properties.Resources.PokemonPicturePath, pokemon.Picture.FrontDefault);

            searchValues = string.Join("|", ObjectId, pokemon.NationalPokedexNumber.ToString("D4"), CobblePedia.Pedia.FR[keyName]).ToLower();

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        public SearchableObjectViewModel(PokemonType pokemonType)
        {
            Source = pokemonType;
            ObjectId = pokemonType.TypeId;
            keyName = pokemonType.KeyName;
            KeyType = "@type";
            label = string.Empty;
            icon = string.Format(Properties.Resources.TypeIconPath, pokemonType.TypeId);

            searchValues = string.Join("|", ObjectId, CobblePedia.Pedia.FR[keyName]).ToLower();

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        public SearchableObjectViewModel(Trainer trainer)
        {
            Source = trainer;
            ObjectId = trainer.TrainerId;
            keyName = trainer.KeyName;
            KeyType = "@trainer";
            label = string.Join(",", trainer.Series);
            icon = "Assets/Trainers/default.png";

            searchValues = string.Join("|", ObjectId, CobblePedia.Pedia.FR[keyName]).ToLower();

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }
        public SearchableObjectViewModel(Move move)
        {
            Source = move;
            ObjectId = move.MoveId;
            keyName = move.KeyName;
            KeyType = "@move";
            label = move.DamageClass;
            icon = string.Format(Properties.Resources.TypeIconPath, move.MoveType);

            searchValues = string.Join("|", ObjectId, CobblePedia.Pedia.FR[keyName]).ToLower();

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        public bool IsElligible(string searchPattern)
        {
            bool isElligible = false;
            bool isKeyTypeFound = false;
            bool isKeyTypeFilterFound = false;

            var querySplit = searchPattern.ToLower().Split(" ");
            foreach (string item in querySplit)
            {
                isKeyTypeFilterFound = isKeyTypeFilterFound || item.StartsWith('@');
                isKeyTypeFound = isKeyTypeFound || item == KeyType;
                if (!isKeyTypeFilterFound && !isKeyTypeFound)
                {
                    isElligible = searchValues.Contains(item);
                }
                if (isKeyTypeFilterFound)
                {
                    if (querySplit.Length == 1)
                    {
                        isElligible = isKeyTypeFound;
                    }
                    else
                    {
                        isElligible = searchValues.Contains(item) && isKeyTypeFound;
                    }
                }

                if (isElligible)
                {
                    continue;
                }
            }

            return isElligible;
        }

        internal void SetLanguage(string language)
        {
            if (Source == null)
            {
                return;
            }

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
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedType"] = ObjectId;
                    result = new TypeExtendedViewModel(Source as PokemonType, null, false);
                    break;
                case "@pokemon":
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedPokemon"] = ObjectId;
                    result = new PokemonExtendedViewModel(Source as Pokemon);
                    break;
                case "@trainer":
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedTrainer"] = ObjectId;
                    result = new TrainerExtendedViewModel(Source as Trainer);
                    break;
                case "@move":
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedMove"] = ObjectId;
                    result = new MoveViewModel(Source as Move, MoveViewModel.EMoveLearnType.None);
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
