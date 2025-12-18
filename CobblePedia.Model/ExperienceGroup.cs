namespace CobblePedia.Models
{
    using global::CobblePedia.Models.Utils;

    using Newtonsoft.Json.Linq;

    public class ExperienceGroup
    {
        public static string FileName { get; } = "experiencegroups.json";
        public static string BasePath { get; } = @"PokeApi\Growth-Rates";

        public string ExperienceGroupId { get; set; }
        public string Formula { get; set; }

        public string KeyDescription
        {
            get
            {
                return string.Format(Properties.Resources.ExperienceGroupKeyDescription, ExperienceGroupId);
            }
        }

        public ExperienceGroup() { }

        public ExperienceGroup(JObject source)
        {
            ExperienceGroupId = JsonHelper.GetStringValue(source, "name").Replace("-", "_");
            Formula = JsonHelper.GetStringValue(source, "formula");
        }
    }
}
