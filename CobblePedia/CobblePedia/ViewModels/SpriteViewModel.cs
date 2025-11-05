namespace CobblePedia.ViewModels
{
    using System.Collections.Generic;

    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    internal partial class SpriteViewModel : ObservableObject
    {
        public enum SpriteCategory
        {
            Default,
            Male,
            Female,
            DefaultShiny,
            MaleShiny,
            FemaleShiny
        }

        [ObservableProperty] private string name;
        [ObservableProperty] private string frontSprite;
        [ObservableProperty] private string backSprite;

        private Pokemon species;
        private SpriteCategory spriteCategory;

        public SpriteViewModel(SpriteCategory category, string front, string back)
        {
            spriteCategory = category;
            frontSprite = string.Format(Properties.Resources.PokemonPicturePath, front);
            backSprite = string.Format(Properties.Resources.PokemonPicturePath, back);

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal void SetLanguage(string language)
        {
            Dictionary<string, string> translation = new Dictionary<string, string>();

            switch (language)
            {
                case "fr":
                    translation = CobblePedia.Pedia.FR;
                    break;
                default:
                    translation = CobblePedia.Pedia.EN;
                    break;
            }

            switch (spriteCategory)
            {
                case SpriteCategory.Default:
                    Name = translation["SpriteDefault"];
                    break;
                case SpriteCategory.Male:
                    Name = translation["SpriteMale"];
                    break;
                case SpriteCategory.Female:
                    Name = translation["SpriteFemale"];
                    break;
                case SpriteCategory.DefaultShiny:
                    Name = translation["SpriteDefaultShiny"];
                    break;
                case SpriteCategory.MaleShiny:
                    Name = translation["SpriteMaleShiny"];
                    break;
                case SpriteCategory.FemaleShiny:
                    Name = translation["SpriteFemaleShiny"];
                    break;
            }
        }

    }
}
