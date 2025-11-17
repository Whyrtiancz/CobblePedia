namespace CobblePedia.ViewModels
{
    using CobblePedia.Models;

    using CommunityToolkit.Mvvm.ComponentModel;

    public partial class SpawnConditionViewModel : ObservableObject
    {
        // https://gitlab.com/cable-mc/cobblemon/-/tree/main/docs/cobblemon-tags/1.6.1
        // Type : toujours Pokemon
        // Context : grounded, submerged, fishing, surface, seafloor, 
        // Bucket : ultra-rare, rare, uncommon, common
        // Presets x0-x1-x2-x3: desert_pyramid, end_city, foliage, illager_structures, jungle_pyramid
        //                      nether_fossil, nether_structures, ocean_monument, ocean_ruins, pillager_outpost, redstone,
        //                      treetop, urban, wild, ancient_city, derelict, illager_structures, lava, mansion, natural,
        //                      redstone, ruined_portal, salt, stronghold, trail_ruins, urban, water, webs, {null}
        //                      https://gitlab.com/cable-mc/cobblemon/-/blob/main/docs/cobblemon-tags/1.6.1/spawnPresetList.md
        // Biomes x5 : https://gitlab.com/cable-mc/cobblemon/-/blob/main/docs/cobblemon-tags/1.6.1/BiomeTags.md

        [ObservableProperty] private string context;
        [ObservableProperty] private string bucket;
        [ObservableProperty] private string presets;
        [ObservableProperty] private string biomes;

        private Spawn spawn;

        public SpawnConditionViewModel(Spawn source)
        {
            spawn = source;

            context = spawn.Context;
            bucket = spawn.Bucket;
            presets = string.Join(", ", spawn.Presets);
            biomes = string.Join(", ", spawn.Biomes);

            SetLanguage((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["DataLanguage"]);
        }

        internal void SetLanguage(string language)
        {
            switch (language)
            {
                case "fr":
                    //Name = CobblePedia.Pedia.FR[ability.KeyName];
                    break;
                default:
                    //Name = CobblePedia.Pedia.EN[ability.KeyName];
                    break;
            }
        }
    }
}
