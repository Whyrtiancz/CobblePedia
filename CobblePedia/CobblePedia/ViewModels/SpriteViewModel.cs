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
            switch (spriteCategory)
            {
                case SpriteCategory.Default:
                    Name = CobblePediaModel.Pedia.Translations[language]["SpriteDefault"];
                    break;
                case SpriteCategory.Male:
                    Name = CobblePediaModel.Pedia.Translations[language]["SpriteMale"];
                    break;
                case SpriteCategory.Female:
                    Name = CobblePediaModel.Pedia.Translations[language]["SpriteFemale"];
                    break;
                case SpriteCategory.DefaultShiny:
                    Name = CobblePediaModel.Pedia.Translations[language]["SpriteDefaultShiny"];
                    break;
                case SpriteCategory.MaleShiny:
                    Name = CobblePediaModel.Pedia.Translations[language]["SpriteMaleShiny"];
                    break;
                case SpriteCategory.FemaleShiny:
                    Name = CobblePediaModel.Pedia.Translations[language]["SpriteFemaleShiny"];
                    break;
            }
        }

    }
}
