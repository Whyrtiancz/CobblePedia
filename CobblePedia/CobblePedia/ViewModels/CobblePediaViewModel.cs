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
        public ObservableCollection<NatureViewModel> NatureViewModels { get; }
        public ObservableCollection<MoveViewModel> MoveViewModels { get; }
        public ObservableCollection<SpeciesViewModel> SpeciesViewModels { get; }
        public ObservableCollection<TrainerViewModel> TrainerViewModels { get; }

        [ObservableProperty] private TypeExtendedViewModel selectedType;
        [ObservableProperty] private SpeciesViewModel selectedSpecies;
        [ObservableProperty] private TrainerViewModel selectedTrainer;

        [ObservableProperty] private int maxBaseHP;
        [ObservableProperty] private int maxBaseAttack;
        [ObservableProperty] private int maxBaseDefence;
        [ObservableProperty] private int maxBaseSpecialAttack;
        [ObservableProperty] private int maxBaseSpecialDefence;
        [ObservableProperty] private int maxBaseSpeed;
        [ObservableProperty] private int maxBaseTotal;

        internal Dictionary<string, GenerationViewModel> generations;
        internal Dictionary<string, TypeExtendedViewModel> types;
        private Dictionary<string, MoveViewModel> moves;
        private Dictionary<string, SpeciesViewModel> species;
        private Dictionary<string, TrainerViewModel> trainers;

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
            moves = new Dictionary<string, MoveViewModel>();
            species = new Dictionary<string, SpeciesViewModel>();
            trainers = new Dictionary<string, TrainerViewModel>();

            foreach (Generation item in CobblePedia.Pedia.Generations.Values)
            {
                generations.Add(item.GenerationId, new GenerationViewModel(item));
            }
            foreach (PokemonType item in CobblePedia.Pedia.Types.Values)
            {
                types.Add(item.TypeId, new TypeExtendedViewModel(item));
            }
            foreach (Move item in CobblePedia.Pedia.Moves.Values)
            {
                moves.Add(item.MoveId, new MoveViewModel(item));
            }
            foreach (Pokemon item in CobblePedia.Pedia.Pokemon.Values.SkipLast(1000))
            {
                species.Add(item.SpeciesId, new SpeciesViewModel(item));
            }
            foreach (Trainer item in CobblePedia.Pedia.Trainers.Values)
            {
                trainers.Add(item.TrainerId, new TrainerViewModel(item));
            }

            maxBaseAttack = CobblePedia.Pedia.Pokemon.Values.Max(item => item.BaseAttack);
            maxBaseDefence = CobblePedia.Pedia.Pokemon.Values.Max(item => item.BaseDefence);
            maxBaseHP = CobblePedia.Pedia.Pokemon.Values.Max(item => item.BaseHP);
            maxBaseSpecialAttack = CobblePedia.Pedia.Pokemon.Values.Max(item => item.BaseSpecialAttack);
            maxBaseSpecialDefence = CobblePedia.Pedia.Pokemon.Values.Max(item => item.BaseSpecialDefence);
            maxBaseSpeed = CobblePedia.Pedia.Pokemon.Values.Max(item => item.BaseSpeed);
            maxBaseTotal = CobblePedia.Pedia.Pokemon.Values.Max(item => item.BaseTotal);

            this.MoveViewModels = new ObservableCollection<MoveViewModel>(moves.Values);
            this.GenerationViewModels = new ObservableCollection<GenerationViewModel>(generations.Values);
            this.TypeViewModels = new ObservableCollection<TypeExtendedViewModel>(types.Values);
            this.SpeciesViewModels = new ObservableCollection<SpeciesViewModel>(species.Values);
            this.TrainerViewModels = new ObservableCollection<TrainerViewModel>(trainers.Values);

            this.NatureViewModels = new ObservableCollection<NatureViewModel>();
            foreach (Nature item in CobblePedia.Pedia.Natures.Values)
            {
                this.NatureViewModels.Add(new NatureViewModel(item));
            }
        }

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
            foreach (NatureViewModel item in this.NatureViewModels)
            {
                item.SetLanguage(language);
            }
            foreach (MoveViewModel item in this.MoveViewModels)
            {
                item.SetLanguage(language);
            }
            foreach (SpeciesViewModel item in this.SpeciesViewModels)
            {
                item.SetLanguage(language);
            }
        }
    }
}
