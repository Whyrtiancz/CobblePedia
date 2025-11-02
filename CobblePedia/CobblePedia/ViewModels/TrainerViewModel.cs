namespace CobblePedia.ViewModels
{
    using System.Collections.ObjectModel;

    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class TrainerViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private int battleCountTicks;
        [ObservableProperty] private int maxTrainerDefeats;
        [ObservableProperty] private int maxTrainerWins;
        [ObservableProperty] private bool isOptional;
        [ObservableProperty] private string signatureItem;
        [ObservableProperty] private float spawnWeightFactor;
        [ObservableProperty] private string trainerType;
        [ObservableProperty] private int maxItemUses;

        public ObservableCollection<Bag> Bags { get; }
        public ObservableCollection<TrainerTeamViewModel> Teams { get; }
        public ObservableCollection<string> BiomeTagBlackList { get; }
        public ObservableCollection<string> BiomeTagWhiteList { get; }
        public ObservableCollection<string> Series { get; }
        public ObservableCollection<string> RequiredDefeats { get; }


        private Trainer trainer;

        public TrainerViewModel(Trainer source)
        {
            trainer = source;
            name = source.TrainerId;
            battleCountTicks = source.BattleCountTicks;
            maxTrainerDefeats = source.MaxTrainerDefeats;
            maxTrainerWins = source.MaxTrainerWins;
            isOptional = source.IsOptional;
            signatureItem = source.SignatureItem;
            spawnWeightFactor = source.SpawnWeightFactor;
            trainerType = source.TrainerType;
            maxItemUses = source.MaxItemUses;

            Bags = new ObservableCollection<Bag>(source.Bags);
            Teams = new ObservableCollection<TrainerTeamViewModel>();
            foreach (TrainerTeam item in source.Teams)
            {
                Teams.Add(new TrainerTeamViewModel(item));
            }
            BiomeTagBlackList = new ObservableCollection<string>(source.BiomeTagBlackList);
            BiomeTagWhiteList = new ObservableCollection<string>(source.BiomeTagWhiteList);
            Series = new ObservableCollection<string>(source.Series);
            RequiredDefeats = new ObservableCollection<string>(source.RequiredDefeats);

            SetLanguage("en");
        }

        internal void SetLanguage(string language)
        {
            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[trainer.KeyName];
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[trainer.KeyName];
                    break;
            }
        }
    }
}
