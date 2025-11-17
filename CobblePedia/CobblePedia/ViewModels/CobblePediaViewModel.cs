namespace CobblePedia.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class CobblePediaViewModel : ObservableObject
    {
        public ObservableCollection<SearchableObjectViewModel> TypeViewModels { get; }
        public ObservableCollection<SearchableObjectViewModel> PokemonViewModels { get; }
        public ObservableCollection<SearchableObjectViewModel> TrainerViewModels { get; }

        [ObservableProperty] private SearchableObjectViewModel? selectedTypeInList;
        [ObservableProperty] private TypeExtendedViewModel? selectedType;
        [ObservableProperty] private SearchableObjectViewModel? selectedPokemonInList;
        [ObservableProperty] private PokemonExtendedViewModel? selectedPokemon;
        [ObservableProperty] private SearchableObjectViewModel selectedTrainerInList;
        [ObservableProperty] private TrainerExtendedViewModel selectedTrainer;
        [ObservableProperty] private MoveViewModel selectedMove;
        [ObservableProperty] private MoveViewModel selectedMoveLeveled;
        [ObservableProperty] private MoveViewModel selectedMoveCT_TM;
        [ObservableProperty] private MoveViewModel selectedMoveTutor;
        [ObservableProperty] private AbilityViewModel selectedAbility;

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
        internal Dictionary<string, TypeSimplifiedViewModel> types;
        internal Dictionary<string, EggGroupViewModel> eggGroups;
        //private Dictionary<string, SearchableObjectViewModel> trainers;
        //private Dictionary<string, SearchableObjectViewModel> pokemon;

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
            foreach (PokemonType item in CobblePedia.Pedia.Types.Values)
            {
                searchableObjects.Add(new SearchableObjectViewModel(item));
            }
            foreach (Pokemon item in CobblePedia.Pedia.Pokemon.Values)
            {
                searchableObjects.Add(new SearchableObjectViewModel(item));
            }
            foreach (Trainer item in CobblePedia.Pedia.Trainers.Values)
            {
                searchableObjects.Add(new SearchableObjectViewModel(item));
            }

            // -------------------------------------------------
            maxBaseAttack = CobblePedia.Pedia.Pokemon.Values.Where(item => !item.IsForm).Max(item => item.BaseAttack);
            maxBaseDefence = CobblePedia.Pedia.Pokemon.Values.Where(item => !item.IsForm).Max(item => item.BaseDefence);
            maxBaseHP = CobblePedia.Pedia.Pokemon.Values.Where(item => !item.IsForm).Max(item => item.BaseHP);
            maxBaseSpecialAttack = CobblePedia.Pedia.Pokemon.Values.Where(item => !item.IsForm).Max(item => item.BaseSpecialAttack);
            maxBaseSpecialDefence = CobblePedia.Pedia.Pokemon.Values.Where(item => !item.IsForm).Max(item => item.BaseSpecialDefence);
            maxBaseSpeed = CobblePedia.Pedia.Pokemon.Values.Where(item => !item.IsForm).Max(item => item.BaseSpeed);

            maxBaseTotal = CobblePedia.Pedia.Pokemon.Values.Where(item => !item.IsForm).Max(item => item.BaseTotal);
            maxBaseTotal = CobblePedia.Pedia.Pokemon.Values.Where(item => !item.IsForm).Max(item => item.BaseTotal);

            maxHeight = CobblePedia.Pedia.Pokemon.Values.Where(item => !item.IsForm).Max(item => item.Height);
            maxWeight = CobblePedia.Pedia.Pokemon.Values.Where(item => !item.IsForm).Max(item => item.Weight);

            // -------------------------------------------------
            // Génération
            generations = new Dictionary<string, GenerationViewModel>();
            foreach (Generation item in CobblePedia.Pedia.Generations.Values)
            {
                generations.Add(item.GenerationId, new GenerationViewModel(item));
            }

            // -------------------------------------------------
            // Types
            types = new Dictionary<string, TypeSimplifiedViewModel>();
            foreach (PokemonType item in CobblePedia.Pedia.Types.Values)
            {
                types.Add(item.TypeId, new TypeSimplifiedViewModel(item));
            }

            // -------------------------------------------------
            // EggGroups
            eggGroups = new Dictionary<string, EggGroupViewModel>();
            foreach (EggGroup item in CobblePedia.Pedia.EggGroups.Values)
            {
                eggGroups.Add(item.EggGroupId, new EggGroupViewModel(item));
            }

            // -------------------------------------------------
            this.TypeViewModels = new ObservableCollection<SearchableObjectViewModel>(searchableObjects.Where(item => item.KeyType == "@type"));
            this.PokemonViewModels = new ObservableCollection<SearchableObjectViewModel>(searchableObjects.Where(item => item.KeyType == "@pokemon" && !((Pokemon)item.Source).IsForm));
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
                SelectedTypeInList = new SearchableObjectViewModel(CobblePedia.Pedia.Types[typeId]);

            }
            if (pokemonId == null)
            {
                SelectedPokemonInList = PokemonViewModels.First();
            }
            else
            {
                SelectedPokemonInList = new SearchableObjectViewModel(CobblePedia.Pedia.Pokemon[pokemonId]);
            }
            if (trainerId == null)
            {
                SelectedTrainerInList = TrainerViewModels.First();
            }
            else
            {
                SelectedTrainerInList = new SearchableObjectViewModel(CobblePedia.Pedia.Trainers[trainerId]);
            }
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
                        SelectedMove = SelectedPokemon.LeveledMoves.First();
                        SelectedAbility = SelectedPokemon.Abilities.First();
                    }
                    break;
                case "SelectedTrainerInList":
                    SelectedTrainer = SelectedTrainerInList.GetExtendedViewModel() as TrainerExtendedViewModel;
                    break;
                case "SelectedMoveLeveled":
                    SelectedMove = SelectedMoveLeveled;
                    break;
                case "SelectedMoveCT_TM":
                    SelectedMove = SelectedMoveCT_TM;
                    break;
                case "SelectedMoveTutor":
                    SelectedMove = SelectedMoveTutor;
                    break;
            }
        }
        #endregion Events
    }
}
