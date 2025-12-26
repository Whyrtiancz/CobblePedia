namespace CobblePedia.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using CobblePedia.Helpers;
    using CobblePedia.Models;
    using CobblePedia.Views;

    using CommunityToolkit.Mvvm.ComponentModel;

    using Microsoft.UI.Dispatching;

    internal partial class CobblePediaViewModel : ObservableObject
    {
        private AsyncPokemonLoader? pokemonLoader;
        private bool isLoadingPokemon = false;
        private AsyncTrainerLoader? trainerLoader;
        private bool isLoadingTrainer = false;

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
            searchableObjects.Add(new SearchableObjectViewModel()); // Empty object for empty damage types

            foreach (PokemonType item in CobblePediaModel.Pedia.Types.Values)
            {
                searchableObjects.Add(new SearchableObjectViewModel(item));
            }
            foreach (Move item in CobblePediaModel.Pedia.Moves.Values)
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
            this.PokemonViewModels = new ObservableCollection<SearchableObjectViewModel>();
            this.TypeViewModels = new ObservableCollection<SearchableObjectViewModel>(searchableObjects.Where(item => item.KeyType == "@type"));
            this.TrainerViewModels = new ObservableCollection<SearchableObjectViewModel>(searchableObjects.Where(item => item.KeyType == "@trainer"));

            // -------------------------------------------------
            PropertyChanged += CobblePediaViewModel_PropertyChanged;
        }

        internal void Initialize()
        {
            SelectedType = new TypeExtendedViewModel(SettingsHelper.GetSelectedType(), false);
            SelectedPokemon = new PokemonExtendedViewModel(CobblePediaModel.Pedia.Pokemons[SettingsHelper.GetSelectedPokemon()]);
            SelectedTrainer = new TrainerExtendedViewModel(CobblePediaModel.Pedia.Trainers[SettingsHelper.GetSelectedTrainer()]);
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

        internal async Task LoadTrainerAsync()
        {
            if (isLoadingTrainer) return;

            isLoadingTrainer= true;

            var dispatcher = DispatcherQueue.GetForCurrentThread();
            trainerLoader = new AsyncTrainerLoader(dispatcher);

            var trainerToLoad = searchableObjects
                .Where(item => item.KeyType == "@trainer")
                .ToList();

            await trainerLoader.LoadTrainerAsync(TrainerViewModels, trainerToLoad);

            isLoadingTrainer = false;
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
        //internal void SetFR()
        //{
        //    SetLanguage("fr");
        //}

        //internal void SetEN()
        //{
        //    SetLanguage("en");
        //}

        private void SetLanguage()
        {
            foreach (GenerationViewModel item in this.generations.Values)
            {
                item.SetLanguage();
            }
            foreach (SearchableObjectViewModel item in this.searchableObjects)
            {
                item.SetLanguage();
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
