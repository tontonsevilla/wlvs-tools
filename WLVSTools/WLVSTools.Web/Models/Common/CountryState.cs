using System.Text.Json.Serialization;

namespace WLVSTools.Web.Models.Common
{
    public class CountryState
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("abbreviation")]
        public string Abbreviation { get; set; }
    }
}
