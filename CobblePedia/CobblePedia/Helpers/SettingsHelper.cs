namespace CobblePedia.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CobblePedia.ViewModels;

    internal static class SettingsHelper
    {
        public static string GetApplicationLanguage()
        {
            string? applicationLanguage = Windows.Storage.ApplicationData.Current.LocalSettings.Values["ApplicationLanguage"] as string;

            if (applicationLanguage is "en-US" or "fr-FR")
            {
                return applicationLanguage;
            }

            return "en-US";
        }

        public static void SetApplicationLanguage(string language)
        {
            if (language == null) 
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["ApplicationLanguage"] = "en-US";
            }
            else 
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["ApplicationLanguage"] = language;
            }
        }

        public static string GetDataLanguage()
        {
            string? dataLanguage = Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"] as string;

            if (dataLanguage is "en" or "fr")
            {
                return dataLanguage;
            }

            return "en";
        }

        public static void SetDataLanguage(string language)
        {
            if (language == null)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"] = "en";
            }
            else
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"] = language;
            }
        }

        public static int GetTheme()
        {
            int? theme = Windows.Storage.ApplicationData.Current.LocalSettings.Values["Theme"] as int?;

            if (theme is null)
            {
                return 0;
            }

            return (int)theme;
        }

        public static void SetTheme(int? theme)
        {
            if (theme is 1 or 0)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["Theme"] = theme;
            }
            else
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["Theme"] = 0;
            }
        }

        public static string GetSelectedType()
        {
            string? typeId = Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedType"] as string;

            return typeId ?? CobblePediaViewModel.Model.TypeViewModels.First().ObjectId;
        }

        public static void SetSelectedType(string? typeId)
        {
            if (typeId == null)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedType"] = CobblePediaViewModel.Model.TypeViewModels.First().ObjectId;
            }
            else
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedType"] = typeId;
            }
        }

        public static string GetSelectedPokemon()
        {
            string? pokemonId = Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedPokemon"] as string;

            return pokemonId ?? CobblePediaViewModel.Model.PokemonViewModels.First().ObjectId;
        }

        public static void SetSelectedPokemon(string? pokemonId)
        {
            if (pokemonId == null)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedPokemon"] = CobblePediaViewModel.Model.PokemonViewModels.First().ObjectId;
            }
            else
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["SelectedPokemon"] = pokemonId;
            }
        }

        public static string GetSelectedTrainer()
        {
            string? trainerId = Windows.Storage.ApplicationData.Current.LocalSettings.Values["Selectedtrainer"] as string;

            return trainerId ?? CobblePediaViewModel.Model.PokemonViewModels.First().ObjectId;
        }

        public static void SetSelectedTrainer(string trainerId)
        {
            if (trainerId == null)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["Selectedtrainer"] = CobblePediaViewModel.Model.TrainerViewModels.First().ObjectId;
            }
            else
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["Selectedtrainer"] = trainerId;
            }
        }
    }
}
