using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BibiBro.Client.Telegram.Helper;
using BibiBro.Client.Telegram.Model;

namespace BibiBro.Client.Telegram.Services
{
    public interface IAutoRuService
    {
        Task<AutoRuData> GetData();
    }

    public class AutoRuService : IAutoRuService
    {
        private readonly HttpClient _client;

        public AutoRuService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://auto.ru/");
        }

        public async Task<AutoRuData> GetData()
        {
            /*
             {"top_days":"1","km_age_to":51000,"seller_group":"PRIVATE","owners_count_group":"ONE","section":"used","category":"cars","sort":"cr_date-desc","output_type":"list","geo_radius":200,"geo_id":[2]}
             {"top_days":"1","km_age_to":51000,"seller_group":"PRIVATE","owners_count_group":"ONE","section":"used","category":"cars","sort":"cr_date-desc","output_type":"list","geo_radius":200,"geo_id":[2]}
             */
            var request = new AutoRuRequest
            {
                TopDays = "1",
                KmAgeTo = 51000,
                SellerGroup = "PRIVATE",
                OwnersCountGroup = "ONE",
                Section = "used",
                Category = "cars",
                Sort = "cr_date-desc",
                OutputType = "list",
                GeoRadius = 200,
                GeoId = new []{2}
            };

            var response = await _client.PostAsync("/-/ajax/desktop/listing/", new JsonContent<AutoRuRequest>(request));

            Console.WriteLine(await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<AutoRuData>(responseStream);
        }
    }
}
