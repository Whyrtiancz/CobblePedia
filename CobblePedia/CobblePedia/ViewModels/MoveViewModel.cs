namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class MoveViewModel : ObservableObject
    {
        [ObservableProperty] private string name;
        [ObservableProperty] private string effect;
        [ObservableProperty] private string flavor;

        [ObservableProperty] private string damageClass;
        [ObservableProperty] private string target;
        [ObservableProperty] private string moveType;

        [ObservableProperty] private string moveTypeIcon;

        private Move move;

        public MoveViewModel(Move source)
        {
            move = source;
            name = "-";
            damageClass = move.DamageClass;
            target = move.Target;
            moveType = move.MoveType;
            moveTypeIcon = string.Format(Properties.Resources.TypeIconPath, moveType);
            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal void SetLanguage(string language)
        {
            switch (language)
            {
                case "fr":
                    Name = CobblePedia.Pedia.FR[move.KeyName];
                    Effect = CobblePedia.Pedia.FR[move.KeyEffect];
                    Flavor = CobblePedia.Pedia.FR[move.KeyFlavor];
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[move.KeyName];
                    Effect = CobblePedia.Pedia.EN[move.KeyEffect];
                    Flavor = CobblePedia.Pedia.EN[move.KeyFlavor];
                    break;
            }
        }
    }

}
