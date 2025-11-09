namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class MoveViewModel : ObservableObject
    {
        internal enum EMoveLearnType
        {
            Level,
            Egg,
            CT_CM,
            Tutor,
            None
        }

        [ObservableProperty] private string name;
        [ObservableProperty] private string description;
        [ObservableProperty] private int atLevel;
        [ObservableProperty] private int accuracy;
        [ObservableProperty] private int power;
        [ObservableProperty] private int pp;
        [ObservableProperty] private int priority;
        [ObservableProperty] private string? effect;
        [ObservableProperty] private string? flavor;

        [ObservableProperty] private string damageClass;
        [ObservableProperty] private string target;
        [ObservableProperty] private string moveType;

        [ObservableProperty] private string moveTypeIcon;
        [ObservableProperty] private string damageClassIcon;
        [ObservableProperty] private string moveTargetIcon;

        private Move move;
        private EMoveLearnType moveLearnType;

        public MoveViewModel(Move source, int level) : this(source, EMoveLearnType.Level)
        {
            atLevel = level;
        }

        public MoveViewModel(Move source, EMoveLearnType moveLearn)
        {
            move = source;
            name = source.KeyName;
            moveLearnType = moveLearn;
            atLevel = -1;
            accuracy = source.Accuracy;
            power = source.Power;
            pp = source.PP;
            priority = source.Priority;
            damageClass = move.DamageClass;
            target = move.Target;
            moveType = move.MoveType;
            moveTypeIcon = string.Format(Properties.Resources.TypeIconPath, moveType);
            damageClassIcon = string.Format(Properties.Resources.DamageClassPicture, damageClass);
            moveTargetIcon = string.Format(Properties.Resources.MoveTargetPicture, target);

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
                    switch (moveLearnType)
                    {
                        case EMoveLearnType.Level:
                            Description = string.Format(CobblePedia.Pedia.FR["Moves_Level"], AtLevel);
                            break;
                        case EMoveLearnType.Egg:
                            Description = CobblePedia.Pedia.FR["Moves_Egg"];
                            break;
                        case EMoveLearnType.CT_CM:
                            Description = CobblePedia.Pedia.FR["Moves_TM"];
                            break;
                        case EMoveLearnType.Tutor:
                            Description = CobblePedia.Pedia.FR["Moves_Tutor"];
                            break;
                        default:
                            Description = string.Empty;
                            break;
                    }
                    break;
                default:
                    Name = CobblePedia.Pedia.EN[move.KeyName];
                    Effect = CobblePedia.Pedia.EN[move.KeyEffect];
                    Flavor = CobblePedia.Pedia.EN[move.KeyFlavor];
                    switch (moveLearnType)
                    {
                        case EMoveLearnType.Level:
                            Description = string.Format(CobblePedia.Pedia.EN["Moves_Level"], AtLevel);
                            break;
                        case EMoveLearnType.Egg:
                            Description = CobblePedia.Pedia.EN["Moves_Egg"];
                            break;
                        case EMoveLearnType.CT_CM:
                            Description = CobblePedia.Pedia.EN["Moves_TM"];
                            break;
                        case EMoveLearnType.Tutor:
                            Description = CobblePedia.Pedia.EN["Moves_Tutor"];
                            break;
                        default:
                            Description = string.Empty;
                            break;
                    }
                    break;
            }
        }
    }
}
