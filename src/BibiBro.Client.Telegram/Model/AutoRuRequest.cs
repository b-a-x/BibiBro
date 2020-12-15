using System.Text.Json;
using System.Text.Json.Serialization;

namespace BibiBro.Client.Telegram.Model
{
    public class AutoRuRequest
    {
        [JsonPropertyName("top_days")]
        public string TopDays { get; set; }

        [JsonPropertyName("km_age_to")]
        public int KmAgeTo { get; set; } 
        
        [JsonPropertyName("seller_group")]
        public string SellerGroup { get; set; }
        
        [JsonPropertyName("owners_count_group")]
        public string OwnersCountGroup { get; set; }
        
        [JsonPropertyName("section")]
        public string Section { get; set; } 
        
        [JsonPropertyName("category")]
        public string Category { get; set; }
        
        [JsonPropertyName("sort")]
        public string Sort { get; set; }
        
        [JsonPropertyName("output_type")]
        public string OutputType { get; set; }
        
        [JsonPropertyName("geo_radius")]
        public int GeoRadius { get; set; }
        
        [JsonPropertyName("geo_id")]
        public int[] GeoId { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
