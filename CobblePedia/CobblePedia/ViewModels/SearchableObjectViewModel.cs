namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    using Windows.Globalization;

    public partial class SearchableObjectViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string icon;
        [ObservableProperty] private string label;
        [ObservableProperty] private string searchLabel;

        public string ObjectId { get; private set; }
        public string KeyType { get; private set; }
        public object Source { get; private set; }
        public string ViewType { get; private set; }

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
            ViewType = string.Empty;

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
            ViewType = "CobblePedia.Views.PokemonView";

            string language = (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"];
            searchValues = string.Join("|", ObjectId, pokemon.NationalPokedexNumber.ToString("D4"), CobblePedia.Pedia.Translations[language][keyName]).ToLower();
            SetLanguage(language);
        }

        public SearchableObjectViewModel(PokemonType pokemonType)
        {
            Source = pokemonType;
            ObjectId = pokemonType.TypeId;
            keyName = pokemonType.KeyName;
            KeyType = "@type";
            label = string.Empty;
            icon = string.Format(Properties.Resources.TypeIconPath, pokemonType.TypeId);
            ViewType = "CobblePedia.Views.TypeView";

            string language = (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"];
            searchValues = string.Join("|", ObjectId, CobblePedia.Pedia.Translations[language][keyName]).ToLower();
            SetLanguage(language);
        }

        public SearchableObjectViewModel(Trainer trainer)
        {
            Source = trainer;
            ObjectId = trainer.TrainerId;
            keyName = trainer.KeyName;
            KeyType = "@trainer";
            label = string.Join(",", trainer.Series);
            icon = "/Assets/Trainers/default.png";
            ViewType = "CobblePedia.Views.TrainerView";

            string language = (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"];
            searchValues = string.Join("|", ObjectId, CobblePedia.Pedia.Translations[language][keyName]).ToLower();
            SetLanguage(language);
        }
        public SearchableObjectViewModel(Move move)
        {
            Source = move;
            ObjectId = move.MoveId;
            keyName = move.KeyName;
            KeyType = "@move";
            label = move.DamageClass;
            icon = string.Format(Properties.Resources.TypeIconPath, move.MoveType);
            ViewType = string.Empty;

            string language = (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"];
            searchValues = string.Join("|", ObjectId, CobblePedia.Pedia.Translations[language][keyName]).ToLower();
            SetLanguage(language);
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

            Name = CobblePedia.Pedia.Translations[language][keyName];
            SearchLabel = string.Format("{0} {1}", KeyType, Name);
        }

        internal ObservableObject GetExtendedViewModel()
        {
            ObservableObject result = null;

            switch (KeyType)
            {
                case "@type":
                    Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedType"] = ObjectId;
                    result = new TypeExtendedViewModel(ObjectId, false);
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

        internal object GetSource()
        {
            return Source;
        }

        public override string ToString()
        {
            return SearchLabel;
        }
    }
}
