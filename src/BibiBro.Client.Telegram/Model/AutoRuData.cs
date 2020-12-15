using System.Text.Json;
using System.Text.Json.Serialization;

namespace BibiBro.Client.Telegram.Model
{
    public class AutoRuData
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
