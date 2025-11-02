namespace CobblePedia.Models.Utils
{
    public static class NamingHelper
    {
        public static string RebuildPokemonName(string value)
        {
            return value.Replace("â€™", "").Replace(" ", "-").Replace(".", "").Replace("-bias", "").ToLower();
        }
    }
}
