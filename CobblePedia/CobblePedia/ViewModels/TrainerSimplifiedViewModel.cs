namespace CobblePedia.ViewModels
{
    using System.Collections.ObjectModel;

    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class TrainerSimplifiedViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string series;
        [ObservableProperty] private string trainerType;
        [ObservableProperty] private bool isOptional;

        //public ObservableCollection<string> Series { get; }
        public ObservableCollection<string> RequiredDefeats { get; }


        private Trainer trainer;

        public TrainerSimplifiedViewModel(Trainer source)
        {
            trainer = source;
            name = source.TrainerId;
            isOptional = source.IsOptional;

            series = string.Join(", ", trainer.Series);
            trainerType = source.TrainerType;
            RequiredDefeats = new ObservableCollection<string>(source.RequiredDefeats);

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal TrainerExtendedViewModel GetExtendedViewModel()
        {
            return new TrainerExtendedViewModel(this.trainer);
        }

        internal void SetLanguage(string language)
        {
            Series = string.Join(", ", trainer.Series);
            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[trainer.KeyName];
                    TrainerType = CobblePedia.Pedia.FR[string.Format(Properties.Resources.TrainerType, trainer.TrainerType)];
                    foreach (string item in trainer.Series)
                    {
                        Series = Series.Replace(item, CobblePedia.Pedia.FR[string.Format(Properties.Resources.TrainerSeries, item)]);
                    }
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[trainer.KeyName];
                    TrainerType = CobblePedia.Pedia.EN[string.Format(Properties.Resources.TrainerType, trainer.TrainerType)];
                    foreach (string item in trainer.Series)
                    {
                        Series = Series.Replace(item, CobblePedia.Pedia.EN[string.Format(Properties.Resources.TrainerSeries, item)]);
                    }
                    break;
            }
        }
    }
}