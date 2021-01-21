using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BibiBro.Client.Telegram.Helper;
using BibiBro.Client.Telegram.Model;
using BibiBro.Client.Telegram.Parser;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace BibiBro.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IParserAutoRu _pars;
        public HomeController(IParserAutoRu parser, ILogger<HomeController> logger)
        {
            _logger = logger;
            _pars = parser;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("DEBUG message. GET enpoint was called.");
            _logger.LogError("Error message. Something went wrong !");
            return Ok("Ok");
        }

        [HttpGet]
        [Route("run")]
        public IActionResult Run()
        {
            _pars.Pars();
            return Ok("Ok");
        }

        [HttpGet]
        [Route("accept")]
        public IActionResult Accept()
        {
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri("https://auto.ru/");
            _client.DefaultRequestHeaders.Add("Accept", "*/*");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            _client.DefaultRequestHeaders.Add("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            _client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            _client.DefaultRequestHeaders.Add("Cookie", "_csrf_token=19203244eef2e2b64f5c4e3548c45da07f7062d6cf7c1c92; autoru_sid=a%3Ag5fd601622a1u00crknnj7eh2ojasgni.09aeccaedf04ee423c0dde200826b2e6%7C1607860578671.604800.nP7ClUDcbsocyFyoF9wY1w.azY-wMgFDsFHVPq990LAmQhIb-0QAspmmiOurS6hAbg; autoruuid=g5fd601622a1u00crknnj7eh2ojasgni.09aeccaedf04ee423c0dde200826b2e6; suid=ab5ed0aff995e5eab77a6846d886f74a.50d8f338582398102219431079e04455; from=direct; yuidcs=1; _ym_uid=1607544350473782789; yuidlt=1; yandexuid=8912904251607860575; promo-app-banner-shown=1; gdpr=0; cycada=q0jOXxkSrKhk6xMc8QQNHkdohMrW8rZi8bIdbNZA0tM=; X-Vertis-DC=vla; _ym_isad=2; spravka=dD0xNjA4MDIxMzA3O2k9OTAuMTU0LjcwLjM2O3U9MTYwODAyMTMwNzMxNzkxMjUwMTtoPWUzMTM2Y2I4NTFjYzFmYjU0MTEyM2QxNmM0YzRiMWFj; proven_owner_popup=closed; _ym_d=1608025598; from_lifetime=1608025598825; promo-header-counter=3");
            //_client.DefaultRequestHeaders.Add("Cookie", "_csrf_token=07c07e1de2071a6df13749a5c01db92512216d644e5fdd65; autoru_sid=a%3Ag5fda26ae27t2v7etslv37348er9u15h.e5c45ca59f0812fa267c306d0a04d923%7C1608132270666.604800.q5j8tbPS37QZ9zbz98FrLg.QU-1AHbUwATMSse89RPuOJyNso5bF8cFtslI2nBxfSA; autoruuid=g5fda26ae27t2v7etslv37348er9u15h.e5c45ca59f0812fa267c306d0a04d923; suid=5e77d309163cd0bde3dff6deed10bb05.e06e12291ed3b3a84908451ba0d3a524; from=direct; yuidcs=1; cmtchd=MTYwODEzMjI3MDY0Mg==; crookie=FawX4rqmZ4+pHkuq3WCso1oAmhy2h3VSl4ahkHENOg0g4JIqtzBSmdFZx5VUEi1LbtT+zYD3Zcra3ZnuxHI48RDW+Tk=; _ym_uid=1608132271229098950; X-Vertis-DC=sas; yuidlt=1; yandexuid=1332181961608131316; counter_ga_all7=2; proven_owner_popup=closed; gdpr=0; promo-app-banner-shown=1; _ga=GA1.2.1312588424.1608406045; _gid=GA1.2.123025598.1608406045; _ym_isad=2; promo-header-counter=2; cycada=3PWSWwiA8sLpgUWXg8h60gJxIiqz2sttlN1qezvFMSo=; _ym_visorc_22753222=b; from_lifetime=1608406104208; _ym_d=1608406104");
            _client.DefaultRequestHeaders.Add("Host", "auto.ru");
            _client.DefaultRequestHeaders.Add("Origin", "https://auto.ru");
            _client.DefaultRequestHeaders.Add("Referer", "https://auto.ru/sankt-peterburg/cars/used/?km_age_to=51000&output_type=list&owners_count_group=ONE&page=1&seller_group=PRIVATE&sort=cr_date-desc&top_days=1");
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Mobile Safari/537.36");
            _client.DefaultRequestHeaders.Add("x-client-app-version", "c2d3fa06d8");
            _client.DefaultRequestHeaders.Add("x-client-date", (((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds() * 1000).ToString());
            _client.DefaultRequestHeaders.Add("x-csrf-token", "19203244eef2e2b64f5c4e3548c45da07f7062d6cf7c1c92");
            //_client.DefaultRequestHeaders.Add("x-csrf-token", "19203244eef2e2b64f5c4e3548c45da07f7062d6cf7c1c92");
            _client.DefaultRequestHeaders.Add("x-page-request-id", "b6e73f65e420cc72642c918d3044d073");
            _client.DefaultRequestHeaders.Add("x-requested-with", "fetch");


            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            //client.DefaultRequestHeaders.Add("x-csrf-token", "19203244eef2e2b64f5c4e3548c45da07f7062d6cf7c1c92");
            //var qwe = client.PostAsync("https://auto.ru/-/ajax/desktop/listing/", null).Result;


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

            var content = new JsonContent<AutoRuRequest>(request);
            
            var response = _client.GetAsync("https://auto.ru/sankt-peterburg/cars/used/?km_age_to=51000&output_type=list&owners_count_group=ONE&page=1&seller_group=PRIVATE&sort=cr_date-desc&top_days=1").Result;
            //_logger.LogError(response.Content.ReadAsStringAsync().Result);
            //var response = _client.PostAsync("/-/ajax/desktop/listing/", content).Result;

            //using var gzstream = new GZipStream(response.Content.ReadAsStream(), CompressionMode.Decompress);
            //Console.WriteLine(JsonSerializer.DeserializeAsync<AutoRuData>(gzstream).Result.ToString() ?? string.Empty);

            using var gzstream = new GZipStream(response.Content.ReadAsStream(), CompressionMode.Decompress);
            using var sr = new StreamReader(gzstream);
            string result = sr.ReadToEnd();
            _logger.LogError(result);

            return Ok("Ok");
        }
    }
}
