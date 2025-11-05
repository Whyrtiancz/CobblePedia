namespace CobblePedia.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class CobblePediaViewModel : ObservableObject
    {
        public ObservableCollection<GenerationViewModel> GenerationViewModels { get; }
        public ObservableCollection<TypeExtendedViewModel> TypeViewModels { get; }
        public ObservableCollection<PokemonSimplifiedViewModel> PokemonViewModels { get; }
        public ObservableCollection<TrainerExtendedViewModel> TrainerViewModels { get; }

        [ObservableProperty] private TypeExtendedViewModel selectedType;
        [ObservableProperty] private PokemonSimplifiedViewModel selectedPokemonInList;
        [ObservableProperty] private PokemonExtendedViewModel selectedPokemon;
        [ObservableProperty] private TrainerExtendedViewModel selectedTrainer;

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
        internal Dictionary<string, TypeExtendedViewModel> types;
        private Dictionary<string, TrainerExtendedViewModel> trainers;
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
            generations = new Dictionary<string, GenerationViewModel>();
            types = new Dictionary<string, TypeExtendedViewModel>();
            pokemon = new Dictionary<string, PokemonSimplifiedViewModel>();
            trainers = new Dictionary<string, TrainerExtendedViewModel>();

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

            foreach (Generation item in CobblePedia.Pedia.Generations.Values)
            {
                generations.Add(item.GenerationId, new GenerationViewModel(item));
            }
            this.GenerationViewModels = new ObservableCollection<GenerationViewModel>(generations.Values);
            foreach (PokemonType item in CobblePedia.Pedia.Types.Values)
            {
                types.Add(item.TypeId, new TypeExtendedViewModel(item));
            }
            this.TypeViewModels = new ObservableCollection<TypeExtendedViewModel>(types.Values);
            foreach (Pokemon item in CobblePedia.Pedia.Pokemon.Values)
            {
                pokemon.Add(item.SpeciesId, new PokemonSimplifiedViewModel(item));
            }
            this.PokemonViewModels = new ObservableCollection<PokemonSimplifiedViewModel>(pokemon.Values.Where(item => !item.IsForm));
            foreach (Trainer item in CobblePedia.Pedia.Trainers.Values)
            {
                trainers.Add(item.TrainerId, new TrainerExtendedViewModel(item));
            }
            this.TrainerViewModels = new ObservableCollection<TrainerExtendedViewModel>(trainers.Values);

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);

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
            foreach (GenerationViewModel item in this.GenerationViewModels)
            {
                item.SetLanguage(language);
            }
            foreach (TypeExtendedViewModel item in this.TypeViewModels)
            {
                item.SetLanguage(language);
            }
            foreach (PokemonSimplifiedViewModel item in this.PokemonViewModels)
            {
                item.SetLanguage(language);
            }
            foreach (TrainerExtendedViewModel item in this.TrainerViewModels)
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
                case "SelectedPokemonInList":
                    SelectedPokemon = new PokemonExtendedViewModel(SelectedPokemonInList.species);
                    break;
            }
        }
        #endregion Events
    }
}
