namespace CobblePedia.Models.Utils
{
    public static class NamingHelper
    {
        public static string RebuildPokemonName(string value)
        {
            string rebuild = value.ToLower();

            rebuild = rebuild.Replace("tea_authenticity=", "");
            rebuild = rebuild.Replace("â€™", "");
            rebuild = rebuild.Replace(" ", "-");
            rebuild = rebuild.Replace(".", "");
            rebuild = rebuild.Replace("-bias", "");
            rebuild = rebuild.Replace("alolan", "alola");
            rebuild = rebuild.Replace("hisuian", "hisui");
            rebuild = rebuild.Replace("galarian", "galar");
            rebuild = rebuild.Replace("mrmime", "mr-mime");
            rebuild = rebuild.Replace("wolf_form=", "");
            rebuild = rebuild.Replace("matcha_authenticity=", "");
            rebuild = rebuild.Replace("sinistea-phony", "sinistea");
            rebuild = rebuild.Replace("polteageist-phony", "polteageist");
            rebuild = rebuild.Replace("poltchageist-counterfeit", "poltchageist");
            rebuild = rebuild.Replace("mrrime", "mr-rime");
            rebuild = rebuild.Replace("porygonz", "porygon-z");
            rebuild = rebuild.Replace("cherrim-blossom_form=overcast", "cherrim-sunshine");
            rebuild = rebuild.Replace("basculegion-male", "basculegion");
            rebuild = rebuild.Replace("basculegion-female", "basculegion-f");
            rebuild = rebuild.Replace("darmanitan-blazing_mode=standard", "darmanitan-zen");
            rebuild = rebuild.Replace("darmanitan-zen-galar", "darmanitan-galar-zen");
            rebuild = rebuild.Replace("darmanitan-blazing_mode=standard-galar", "darmaniitan-galar");
            rebuild = rebuild.Replace("vivillon-vivillon_wings=poke-ball", "vivillon-pokeball");
            rebuild = rebuild.Replace("hakamoo", "hakamo-o");
            rebuild = rebuild.Replace("kommoo", "kommo-o");
            rebuild = rebuild.Replace("alcremie-decoration=strawberry-cream=", "alcremie-");
            rebuild = rebuild.Replace("alcremie-decoration=love-cream=", "alcremie-");
            rebuild = rebuild.Replace("alcremie-decoration=berry-cream=", "alcremie-");
            rebuild = rebuild.Replace("alcremie-decoration=clover-cream=", "alcremie-");
            rebuild = rebuild.Replace("alcremie-decoration=flower-cream=", "alcremie-");
            rebuild = rebuild.Replace("alcremie-decoration=star-cream=", "alcremie-");
            rebuild = rebuild.Replace("alcremie-decoration=ribbon-cream=", "alcremie-");

            rebuild = rebuild.Replace("alcremie-vanilla", "alcremie");
            rebuild = rebuild.Replace("alcremie-salted", "alcremie-salted-cream");
            rebuild = rebuild.Replace("alcremie-lemon", "alcremie-lemon-cream");
            rebuild = rebuild.Replace("alcremie-ruby", "alcremie-ruby-cream");
            rebuild = rebuild.Replace("alcremie-matcha", "alcremie-matcha-cream");
            rebuild = rebuild.Replace("alcremie-mint", "alcremie-mint-cream");
            rebuild = rebuild.Replace("ruby_swirl", "ruby-swirl");
            rebuild = rebuild.Replace("rainbow_swirl", "rainbow-swirl");
            rebuild = rebuild.Replace("cream_swirl", "swirl");
            rebuild = rebuild.Replace("caramel_swirl", "caramel-swirl");

            rebuild = rebuild.Replace("meowstic-male", "meowstic");
            rebuild = rebuild.Replace("meowstic-female", "meowstic-f");

            rebuild = rebuild.Replace("aegislash-stance_forme=shield", "aegislash-blade");
            rebuild = rebuild.Replace("lycanroc-midday", "lycanroc-midnight");
            rebuild = rebuild.Replace("oinkologne-male", "oinkologne");
            rebuild = rebuild.Replace("oinkologne-female", "oinkologne-f");
            rebuild = rebuild.Replace("urshifu-wushu_style=rapid_strike", "urshifu-rapid-strike");
            rebuild = rebuild.Replace("urshifu-wushu_style=single_strike", "urshifu-rapid-strike");
            rebuild = rebuild.Replace("sinistcha-unremarkable", "sinistcha-masterpiece");

            return rebuild;
        }
    }
}
