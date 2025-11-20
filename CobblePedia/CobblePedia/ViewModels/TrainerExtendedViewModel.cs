namespace CobblePedia.ViewModels
{
    using System.Collections.ObjectModel;

    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class TrainerExtendedViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private int battleCountTicks;
        [ObservableProperty] private int maxTrainerDefeats;
        [ObservableProperty] private int maxTrainerWins;
        [ObservableProperty] private bool isOptional;
        [ObservableProperty] private ItemViewModel signatureItem;
        [ObservableProperty] private float spawnWeightFactor;
        [ObservableProperty] private string trainerType;
        [ObservableProperty] private string series;
        [ObservableProperty] private int maxItemUses;

        public ObservableCollection<BagViewModel> Bags { get; }
        public ObservableCollection<TrainerTeamViewModel> Teams { get; }
        public ObservableCollection<string> BiomeTagBlackList { get; }
        public ObservableCollection<string> BiomeTagWhiteList { get; }
        public ObservableCollection<string> RequiredDefeats { get; }

        private string KeyName;
        private string trainerTypeKey;
        private string serieKey;
        private string serieDescriptionKey;
        private string signatureItemKey;

        public TrainerExtendedViewModel(Trainer source)
        {
            name = source.TrainerId;
            battleCountTicks = source.BattleCountTicks;
            maxTrainerDefeats = source.MaxTrainerDefeats;
            maxTrainerWins = source.MaxTrainerWins;
            isOptional = source.IsOptional;

            signatureItem = new ItemViewModel();
            if (source.SignatureItem != null)
            {
                signatureItem = new ItemViewModel(source.SignatureItem);
            }

            spawnWeightFactor = source.SpawnWeightFactor;
            series = string.Join(",", source.Series);
            serieKey = string.Format(Properties.Resources.TrainerSeries_Title, "empty");
            serieDescriptionKey = string.Format(Properties.Resources.TrainerSeries_Description, "empty");
            if (source.Series.Count > 0)
            {
                serieKey = string.Format(Properties.Resources.TrainerSeries_Title, source.Series[0]);
                serieDescriptionKey = string.Format(Properties.Resources.TrainerSeries_Description, series);
            }
            trainerTypeKey = "trainer_type.rctmod.unknown.title";
            if (source.TrainerType != null)
            {
                trainerTypeKey = string.Format(Properties.Resources.TrainerType_Title, source.TrainerType);
            }
            maxItemUses = source.MaxItemUses;

            KeyName = source.KeyName;

            Bags = new ObservableCollection<BagViewModel>();
            foreach (Bag item in source.Bags)
            {
                Bags.Add(new BagViewModel(item));
            }

            Teams = new ObservableCollection<TrainerTeamViewModel>();
            foreach (TrainerTeam item in source.Teams)
            {
                Teams.Add(new TrainerTeamViewModel(item));
            }

            BiomeTagBlackList = new ObservableCollection<string>(source.BiomeTagBlackList);
            BiomeTagWhiteList = new ObservableCollection<string>(source.BiomeTagWhiteList);
            RequiredDefeats = new ObservableCollection<string>(source.RequiredDefeats);

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal void SetLanguage(string language)
        {
            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[KeyName];
                    Series = CobblePedia.Pedia.FR[serieKey];
                    TrainerType = CobblePedia.Pedia.FR[trainerTypeKey];
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[KeyName];
                    Series = CobblePedia.Pedia.EN[serieKey];
                    TrainerType = CobblePedia.Pedia.EN[trainerTypeKey];
                    break;
            }
        }
    }
}
