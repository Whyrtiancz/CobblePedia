namespace CobblePedia.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class CobblePediaViewModel : ObservableObject
    {
        public ObservableCollection<TypeSimplifiedViewModel> TypeViewModels { get; }
        public ObservableCollection<PokemonSimplifiedViewModel> PokemonViewModels { get; }
        public ObservableCollection<TrainerSimplifiedViewModel> TrainerViewModels { get; }

        [ObservableProperty] private TypeSimplifiedViewModel selectedTypeInList;
        [ObservableProperty] private TypeExtendedViewModel selectedType;
        [ObservableProperty] private PokemonSimplifiedViewModel selectedPokemonInList;
        [ObservableProperty] private PokemonExtendedViewModel selectedPokemon;
        [ObservableProperty] private TrainerSimplifiedViewModel selectedTrainerInList;
        [ObservableProperty] private TrainerExtendedViewModel selectedTrainer;
        [ObservableProperty] private MoveViewModel selectedMove;
        [ObservableProperty] private MoveViewModel selectedMoveLeveled;
        [ObservableProperty] private MoveViewModel selectedMoveCT_TM;
        [ObservableProperty] private MoveViewModel selectedMoveTutor;

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
        private Dictionary<string, TrainerSimplifiedViewModel> trainers;
        private Dictionary<string, PokemonSimplifiedViewModel> pokemon;

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
            this.TypeViewModels = new ObservableCollection<TypeSimplifiedViewModel>(types.Values);

            // -------------------------------------------------
            // EggGroups
            eggGroups = new Dictionary<string, EggGroupViewModel>();

            foreach (EggGroup item in CobblePedia.Pedia.EggGroups.Values)
            {
                eggGroups.Add(item.EggGroupId, new EggGroupViewModel(item));
            }

            // -------------------------------------------------
            // Pokemon
            pokemon = new Dictionary<string, PokemonSimplifiedViewModel>();
            foreach (Pokemon item in CobblePedia.Pedia.Pokemon.Values)
            {
                pokemon.Add(item.SpeciesId, new PokemonSimplifiedViewModel(item));
            }
            this.PokemonViewModels = new ObservableCollection<PokemonSimplifiedViewModel>(pokemon.Values.Where(item => !item.IsForm));

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
            // Trainers
            trainers = new Dictionary<string, TrainerSimplifiedViewModel>();

            foreach (Trainer item in CobblePedia.Pedia.Trainers.Values)
            {
                trainers.Add(item.TrainerId, new TrainerSimplifiedViewModel(item));
            }
            this.TrainerViewModels = new ObservableCollection<TrainerSimplifiedViewModel>(trainers.Values);

            // -------------------------------------------------
            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);

            // -------------------------------------------------
            PropertyChanged += CobblePediaViewModel_PropertyChanged;
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
            foreach (TypeSimplifiedViewModel item in this.TypeViewModels)
            {
                item.SetLanguage(language);
            }
            foreach (GenerationViewModel item in this.generations.Values)
            {
                item.SetLanguage(language);
            }
            foreach (PokemonSimplifiedViewModel item in this.PokemonViewModels)
            {
                item.SetLanguage(language);
            }
            foreach (TrainerSimplifiedViewModel item in this.TrainerViewModels)
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
                    SelectedType = SelectedTypeInList.GetExtendedViewModel();
                    break;
                case "SelectedPokemonInList":
                    SelectedPokemon = SelectedPokemonInList.GetExtendedViewModel();
                    break;
                case "SelectedTrainerInList":
                    SelectedTrainer = SelectedTrainerInList.GetExtendedViewModel();
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
