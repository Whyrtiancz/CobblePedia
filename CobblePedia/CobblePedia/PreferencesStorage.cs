namespace CobblePedia
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Windows.Storage;

    internal static class PreferencesStorage
    {
        private const string fileName = "cobblepedia.userprefs.json";

        private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web) { WriteIndented = true };

        internal static async Task<UserPreferences> LoadAsync()
        {
            var folder = ApplicationData.Current.LocalFolder;
            var exists = await folder.TryGetItemAsync(fileName);
            if (exists is StorageFile file)
            {
                var json = await FileIO.ReadTextAsync(file);
                return JsonSerializer.Deserialize<UserPreferences>(json, Options) ?? new();
            }
            return new();
        }

        internal static async Task SaveAsync(UserPreferences prefs)
        {
            var folder = ApplicationData.Current.LocalFolder;
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            var json = JsonSerializer.Serialize(prefs, Options);
            await FileIO.WriteTextAsync(file, json);
        }
    }
}
