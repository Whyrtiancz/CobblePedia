namespace CobblePedia.ViewModels
{
    using CobblePedia.Helpers;
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

        //private Move move;
        private string translationKey;
        private string translationEffectKey;
        private string translationFlavorKey;

        private EMoveLearnType moveLearnType;

        public MoveViewModel(Move source, int level) : this(source, EMoveLearnType.Level)
        {
            atLevel = level;
        }

        public MoveViewModel(Move source, EMoveLearnType moveLearn)
        {
            translationKey = source.KeyName;
            translationEffectKey = source.KeyEffect;
            translationFlavorKey = source.KeyFlavor;
            name = source.KeyName;
            description = string.Empty;
            moveLearnType = moveLearn;
            atLevel = -1;
            accuracy = source.Accuracy;
            power = source.Power;
            pp = source.PP;
            priority = source.Priority;
            damageClass = source.DamageClass;
            target = source.Target;
            moveType = source.MoveType;
            moveTypeIcon = string.Format(Properties.Resources.TypeIconPath, moveType);
            damageClassIcon = string.Format(Properties.Resources.DamageClassPicture, damageClass);
            moveTargetIcon = string.Format(Properties.Resources.MoveTargetPicture, target);

            SetLanguage();
        }

        internal void SetLanguage()
        {
            Name = CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()][translationKey];
            Effect = CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()][translationEffectKey];
            Flavor = CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()][translationFlavorKey];
            switch (moveLearnType)
            {
                case EMoveLearnType.Level:
                    Description = string.Format(CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()]["Moves_Level"], AtLevel);
                    break;
                case EMoveLearnType.Egg:
                    Description = CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()]["Moves_Egg"];
                    break;
                case EMoveLearnType.CT_CM:
                    Description = CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()]["Moves_TM"];
                    break;
                case EMoveLearnType.Tutor:
                    Description = CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()]["Moves_Tutor"];
                    break;
                default:
                    Description = string.Empty;
                    break;
            }
        }
    }
}
