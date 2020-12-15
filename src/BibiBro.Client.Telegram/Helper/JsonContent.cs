using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace BibiBro.Client.Telegram.Helper
{
    public class JsonContent<T> : StringContent where T : class
    {
        public JsonContent(T value) : base(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
        {
        }

        public JsonContent(T value, string mediaType) : base(JsonSerializer.Serialize(value), Encoding.UTF8, mediaType)
        {
        }
    }
}
