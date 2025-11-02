namespace CobblePedia.Models.Cobblemon
{
    internal sealed class RctmodJar
    {
        internal static readonly string[] Paths =
        {
            "assets/rctmod/lang",
            "data/rctmod/advancement",
            "data/rctmod/loot_table",
            "data/rctmod/mobs",
            "data/rctmod/series",
            "data/rctmod/trainer_type",
            "data/rctmod/trainers"
        };

        internal static readonly string BattleInfoPath = Path.Combine(Paths[3], "trainers/single");
    }
}
