namespace CobblePedia.ViewModels
{
    using System.Collections.Generic;

    using CobblePedia.Helpers;
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class EvolutionViewModel : ObservableObject
    {
        [ObservableProperty] private SearchableObjectViewModel evolveTo;
        [ObservableProperty] private string method;

        private string methodKey;
        private string methodValue;
        private int minLevel;
        private int minFriendship;
        private int minBattleCriticalHits;
        private string itemKey;

        public EvolutionViewModel()
        {
            method = string.Empty;
            methodKey = string.Empty;
            methodValue = string.Empty;
            minLevel = 0;
            minFriendship = 0;
            minBattleCriticalHits = 0;
            itemKey = string.Empty;
        }

        public EvolutionViewModel(Evolution source)
        {
            method = string.Empty;
            methodKey = string.Format("Evolution_Method_{0}", source.Method);
            methodValue = source.Method;
            minLevel = source.MinLevel;
            minFriendship = source.MinFriendship;
            minBattleCriticalHits = source.MinBattleCriticalHits;
            itemKey = source.ItemKey;

            evolveTo = CobblePediaViewModel.Model.GetSearchableObjectViewModel(source.EvolveTo, "@pokemon");

            SetLanguage();
        }

        internal void SetLanguage()
        {
            switch (methodValue)
            {
                case "level_up":
                    Method = string.Format(CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()][methodKey], minLevel);
                    break;
                case "friendship":
                    Method = string.Format(CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()][methodKey], minFriendship);
                    break;
                case "battle_critical_hits":
                    Method = string.Format(CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()][methodKey], minBattleCriticalHits);
                    break;
                case "item_interact":
                    Method = string.Format(CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()][methodKey], CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()][itemKey]);
                    break;
                case "trade":
                    Method = string.Format(CobblePediaModel.Pedia.Translations[SettingsHelper.GetDataLanguage()][methodKey], "");
                    break;
            }
        }
    }
}