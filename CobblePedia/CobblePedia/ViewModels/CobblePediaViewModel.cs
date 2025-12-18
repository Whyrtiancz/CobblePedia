namespace CobblePedia.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    using Microsoft.UI.Dispatching;

    internal partial class CobblePediaViewModel : ObservableObject
    {
        // Ajouter en début de classe
        private AsyncPokemonLoader? pokemonLoader;
        private bool isLoadingPokemon = false;

        public ObservableCollection<SearchableObjectViewModel> TypeViewModels { get; }
        public ObservableCollection<SearchableObjectViewModel> PokemonViewModels { get; }
        public ObservableCollection<SearchableObjectViewModel> TrainerViewModels { get; }

        [ObservableProperty] private SearchableObjectViewModel? selectedTypeInList;
        [ObservableProperty] private TypeExtendedViewModel? selectedType;

        [ObservableProperty] private SearchableObjectViewModel selectedTrainerInList;
        [ObservableProperty] private TrainerExtendedViewModel selectedTrainer;

        [ObservableProperty] private SearchableObjectViewModel? selectedMoveInList;
        [ObservableProperty] private MoveViewModel? selectedMove;

        [ObservableProperty] private SearchableObjectViewModel? selectedPokemonInList;
        [ObservableProperty] private PokemonExtendedViewModel? selectedPokemon;
        [ObservableProperty] private MoveViewModel? pokemonSelectedMove;
        [ObservableProperty] private MoveViewModel? selectedMoveLeveled;
        [ObservableProperty] private MoveViewModel? selectedMoveCT_TM;
        [ObservableProperty] private MoveViewModel? selectedMoveTutor;
        [ObservableProperty] private AbilityViewModel? selectedAbility;

        [ObservableProperty] private int maxBaseHP;
        [ObservableProperty] private int maxBaseAttack;
        [ObservableProperty] private int maxBaseDefence;
        [ObservableProperty] private int maxBaseSpecialAttack;
        [ObservableProperty] private int maxBaseSpecialDefence;
        [ObservableProperty] private int maxBaseSpeed;
        [ObservableProperty] private int maxBaseTotal;
        [ObservableProperty] private int maxHeight;
        [ObservableProperty] private int maxWeight;

        internal Dictionary<string, GenerationViewModel> generations;
        internal Dictionary<string, EggGroupViewModel> eggGroups;

        List<SearchableObjectViewModel> searchableObjects;

        private static CobblePediaViewModel model;

        public static CobblePediaViewModel Model
        {
            get
            {
                if (model == null)
                {
                    model = new CobblePediaViewModel();
                }
                return model;
            }
        }

        private CobblePediaViewModel()
        {
            // -------------------------------------------------
            searchableObjects = new List<SearchableObjectViewModel>();
            searchableObjects.Add(new SearchableObjectViewModel());

            foreach (PokemonType item in CobblePediaModel.Pedia.Types.Values)
            {
                searchableObjects.Add(new SearchableObjectViewModel(item));
            }
            foreach (Pokemon item in CobblePediaModel.Pedia.Pokemons.Values)
            {
                searchableObjects.Add(new SearchableObjectViewModel(item));
            }
            foreach (Trainer item in CobblePediaModel.Pedia.Trainers.Values)
            {
                searchableObjects.Add(new SearchableObjectViewModel(item));
            }
            foreach (Move item in CobblePediaModel.Pedia.Moves.Values)
            {
                searchableObjects.Add(new SearchableObjectViewModel(item));
            }

            // -------------------------------------------------
            maxBaseAttack = CobblePediaModel.Pedia.Pokemons.Values.Where(item => !item.IsForm).Max(item => item.BaseAttack);
            maxBaseDefence = CobblePediaModel.Pedia.Pokemons.Values.Where(item => !item.IsForm).Max(item => item.BaseDefence);
            maxBaseHP = CobblePediaModel.Pedia.Pokemons.Values.Where(item => !item.IsForm).Max(item => item.BaseHP);
            maxBaseSpecialAttack = CobblePediaModel.Pedia.Pokemons.Values.Where(item => !item.IsForm).Max(item => item.BaseSpecialAttack);
            maxBaseSpecialDefence = CobblePediaModel.Pedia.Pokemons.Values.Where(item => !item.IsForm).Max(item => item.BaseSpecialDefence);
            maxBaseSpeed = CobblePediaModel.Pedia.Pokemons.Values.Where(item => !item.IsForm).Max(item => item.BaseSpeed);

            maxBaseTotal = CobblePediaModel.Pedia.Pokemons.Values.Where(item => !item.IsForm).Max(item => item.BaseTotal);
            maxBaseTotal = CobblePediaModel.Pedia.Pokemons.Values.Where(item => !item.IsForm).Max(item => item.BaseTotal);

            maxHeight = CobblePediaModel.Pedia.Pokemons.Values.Where(item => !item.IsForm).Max(item => item.Height);
            maxWeight = CobblePediaModel.Pedia.Pokemons.Values.Where(item => !item.IsForm).Max(item => item.Weight);

            // -------------------------------------------------
            // Génération
            generations = new Dictionary<string, GenerationViewModel>();
            foreach (Generation item in CobblePediaModel.Pedia.Generations.Values)
            {
                generations.Add(item.GenerationId, new GenerationViewModel(item));
            }

            // -------------------------------------------------
            // EggGroups
            eggGroups = new Dictionary<string, EggGroupViewModel>();
            foreach (EggGroup item in CobblePediaModel.Pedia.EggGroups.Values)
            {
                eggGroups.Add(item.EggGroupId, new EggGroupViewModel(item));
            }

            // -------------------------------------------------
            //this.TypeViewModels = new ObservableCollection<SearchableObjectViewModel>(searchableObjects.Where(item => item.KeyType == "@type"));
            //this.PokemonViewModels = new ObservableCollection<SearchableObjectViewModel>(searchableObjects.Where(item => item.KeyType == "@pokemon" && !((Pokemon)item.Source).IsForm));
            //this.TrainerViewModels = new ObservableCollection<SearchableObjectViewModel>(searchableObjects.Where(item => item.KeyType == "@trainer"));

            this.PokemonViewModels = new ObservableCollection<SearchableObjectViewModel>();
            this.TypeViewModels = new ObservableCollection<SearchableObjectViewModel>(searchableObjects.Where(item => item.KeyType == "@type"));
            this.TrainerViewModels = new ObservableCollection<SearchableObjectViewModel>(searchableObjects.Where(item => item.KeyType == "@trainer"));

            // -------------------------------------------------
            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);

            // -------------------------------------------------
            PropertyChanged += CobblePediaViewModel_PropertyChanged;
        }

        internal void Initialize()
        {
            string? typeId = Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedType"] as string;
            string? pokemonId = Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedPokemon"] as string;
            string? trainerId = Windows.Storage.ApplicationData.Current.LocalSettings.Values["Selectedtrainer"] as string;

            if (typeId == null)
            {
                SelectedTypeInList = TypeViewModels.First();
            }
            else
            {
                SelectedTypeInList = new SearchableObjectViewModel(CobblePediaModel.Pedia.Types[typeId]);

            }
            if (pokemonId == null)
            {
                SelectedPokemonInList = PokemonViewModels.First();
            }
            else
            {
                SelectedPokemonInList = new SearchableObjectViewModel(CobblePediaModel.Pedia.Pokemons[pokemonId]);
            }
            if (trainerId == null)
            {
                SelectedTrainerInList = TrainerViewModels.First();
            }
            else
            {
                SelectedTrainerInList = new SearchableObjectViewModel(CobblePediaModel.Pedia.Trainers[trainerId]);
            }
        }

        internal async Task LoadPokemonAsync()
        {
            if (isLoadingPokemon) return;

            isLoadingPokemon = true;

            var dispatcher = DispatcherQueue.GetForCurrentThread();
            pokemonLoader = new AsyncPokemonLoader(dispatcher);

            var pokemonToLoad = searchableObjects
                .Where(item => item.KeyType == "@pokemon" && !((Pokemon)item.Source).IsForm)
                .ToList();

            await pokemonLoader.LoadPokemonAsync(PokemonViewModels, pokemonToLoad);

            isLoadingPokemon = false;
        }

        internal SearchableObjectViewModel GetSearchableObjectViewModel(string objectId, string keyType)
        {
            return searchableObjects.Where(item => item.ObjectId == objectId && item.KeyType == keyType).First();
        }

        internal List<SearchableObjectViewModel> Filter(string searchPattern)
        {
            List<SearchableObjectViewModel> elligibles = new List<SearchableObjectViewModel>();

            foreach (SearchableObjectViewModel item in searchableObjects)
            {
                if (item.IsElligible(searchPattern))
                    elligibles.Add(item);
            }

            return elligibles;
        }

        #region Data Language
        internal void SetFR()
        {
            SetLanguage("fr");
        }

        internal void SetEN()
        {
            SetLanguage("en");
        }

        private void SetLanguage(string language)
        {
            foreach (GenerationViewModel item in this.generations.Values)
            {
                item.SetLanguage(language);
            }
            foreach (SearchableObjectViewModel item in this.searchableObjects)
            {
                item.SetLanguage(language);
            }
        }
        #endregion Data Language

        #region Events
        private void CobblePediaViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SelectedTypeInList":
                    SelectedType = SelectedTypeInList.GetExtendedViewModel() as TypeExtendedViewModel;
                    break;
                case "SelectedPokemonInList":
                    SelectedPokemon = SelectedPokemonInList.GetExtendedViewModel() as PokemonExtendedViewModel;
                    if (SelectedPokemon != null)
                    {
                        PokemonSelectedMove = SelectedPokemon.LeveledMoves.First();
                        SelectedAbility = SelectedPokemon.Abilities.First();
                    }
                    break;
                case "SelectedTrainerInList":
                    SelectedTrainer = SelectedTrainerInList.GetExtendedViewModel() as TrainerExtendedViewModel;
                    break;
                case "SelectedMoveLeveled":
                    PokemonSelectedMove = SelectedMoveLeveled;
                    break;
                case "SelectedMoveCT_TM":
                    PokemonSelectedMove = SelectedMoveCT_TM;
                    break;
                case "SelectedMoveTutor":
                    PokemonSelectedMove = SelectedMoveTutor;
                    break;
            }
        }
        #endregion Events
    }
}
