namespace CobblePedia.Models.Utils
{
    using System.Linq;

    using Newtonsoft.Json.Linq;

    public static class JsonHelper
    {
        public static string GetTranslation(JObject translation, string name)
        {
            if (null == translation)
                return null;

            if (translation.ContainsKey(name))
            {
                return translation[name].Value<string>();
            }

            return null;
        }
        public static string GetTranslation(JObject translation, string language, string name)
        {
            if (null == translation)
                return null;

            var values = translation.SelectTokens(string.Format("$.names[?(@.language.name=='{0}')].{1}", language, name))?.Values<string>().ToList<string>();
            if (values.Count > 0)
            {
                return values[0];
            }

            return null;
        }

        public static string GetEffectChange(JObject translation, string language)
        {
            if (null == translation)
                return null;

            var values = translation.SelectTokens(string.Format("$.effect_changes.effect_entries[?(@.language.name=='{0}')].effect", language))?.Values<string>().ToList<string>();
            if (values.Count > 0)
            {
                return values[0];
            }

            return null;
        }

        public static string GetEffectEntry(JObject translation, string language)
        {
            if (null == translation)
                return null;

            var values = translation.SelectTokens(string.Format("$.effect_entries[?(@.language.name=='{0}')].effect", language))?.Values<string>().ToList<string>();
            if (values.Count > 0)
            {
                return values[0];
            }

            return null;
        }
        public static string GetFlavorEntry(JObject translation, string language)
        {
            if (null == translation)
                return null;

            var values = translation.SelectTokens(string.Format("$.flavor_text_entries[?(@.language.name=='{0}')].flavor_text", language))?.Values<string>().ToList<string>();
            if (values.Count > 0)
            {
                return values[0];
            }

            return null;
        }

        public static bool GetBoolValue(JObject source, string key)
        {
            if (source.ContainsKey(key))
            {
                return (bool)source.SelectToken(key);
            }

            return false;
        }
        public static float GetFloatValue(JObject source, string key)
        {
            if (source.ContainsKey(key))
            {
                return (float)source.SelectToken(key);
            }

            return 0;
        }
        public static int GetIntValue(JObject source, string key)
        {
            if (source.ContainsKey(key))
            {
                return (int)source.SelectToken(key);
            }

            return 0;
        }
        public static string GetStringValue(JObject source, string key)
        {
            if (source.ContainsKey(key))
            {
                return (string)source.SelectToken(key);
            }

            return null;
        }
        public static int GetIntValue(JObject source, string key, string subkey)
        {
            if (source.ContainsKey(key))
            {
                return (int)source.SelectToken(subkey);
            }

            return 0;
        }
        public static string GetStringValue(JObject source, string key, string subkey)
        {
            if (source.ContainsKey(key))
            {
                return (string)source.SelectToken(subkey);
            }

            return null;
        }

    }
}
