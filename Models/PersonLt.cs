using System.Text.Json.Serialization;

namespace NuclearWinter.Models
{
    public class PersonLt
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("Gender")]
        public string Gender { get; set; }
    }
}