using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Text;
using BibiBro.Client.Telegram.Model;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BibiBro.Client.Telegram.Parser
{
    public interface IParserAutoRu
    {
        Task Pars();
    }

    //TODO: Разобраться как получить результат работы задачи, вынести httpClient
    public class ParserAutoRu : IParserAutoRu //,IJob
    {
        private readonly ITelegramManager _manager;
        public ParserAutoRu(ITelegramManager manager)
        {
            _manager = manager;
        }

        //public Task Execute(IJobExecutionContext context)
        public Task Pars()
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage responce;
            try
            {
                responce = client
                    .GetAsync(
                        "https://auto.ru/sankt-peterburg/cars/used/?km_age_to=51000&output_type=list&owners_count_group=ONE&page=1&seller_group=PRIVATE&sort=cr_date-desc&top_days=1")
                    .Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.CompletedTask;
            }

            if (responce.IsSuccessStatusCode == false)
            {
                Console.WriteLine($"Запрос выполнился с ошибкой {DateTime.Now}");
                return Task.CompletedTask;
            }

            var source = responce.Content.ReadAsStringAsync().Result;

            var parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(source);
            IEnumerable<IElement> elements =
                document.QuerySelectorAll("div").Where(e => e.ClassName == "ListingItem-module__main");

            Console.WriteLine($"Количество элементов {elements.Count()}");

            var result = new List<Advertisement>();
            foreach (IElement element in elements)
            {
                var dateElement = element.GetElementsByClassName("MetroListPlace__content MetroListPlace_nbsp")
                    .FirstOrDefault();

                if (dateElement == null)
                    continue;

                int minutes = int.MaxValue;
                if (dateElement.TextContent.Contains("минуты"))
                {
                    var str = dateElement.TextContent.SplitSpaces()[0];
                    minutes = str.Substring(0, str.Length - 7).ToInteger(1000);
                }
                else if (dateElement.TextContent.Contains("минута"))
                {
                    var str = dateElement.TextContent.SplitSpaces()[0];
                    minutes = str.Substring(0, str.Length - 6).ToInteger(1000);
                }
                else if (dateElement.TextContent.Contains("минут"))
                {
                    var str = dateElement.TextContent.SplitSpaces()[0];
                    minutes = str.Substring(0, str.Length - 6).ToInteger(1000);
                }
                else if (dateElement.TextContent.Contains("часов"))
                {
                    var str = dateElement.TextContent.SplitSpaces()[0];
                    minutes = str.Substring(0, str.Length - 6).ToInteger(1000) * 60;
                }
                else if (dateElement.TextContent.Contains("часы"))
                {
                    var str = dateElement.TextContent.SplitSpaces()[0];
                    minutes = str.Substring(0, str.Length - 5).ToInteger(1000) * 60;
                }
                else if (dateElement.TextContent.Contains("часа"))
                {
                    var str = dateElement.TextContent.SplitSpaces()[0];
                    minutes = str.Substring(0, str.Length - 5).ToInteger(1000) * 60;
                }
                else if (dateElement.TextContent.Contains("час"))
                {
                    var str = dateElement.TextContent.SplitSpaces()[0];
                    minutes = str.Substring(0, str.Length - 4).ToInteger(1000) * 60;
                }

                if (30 < minutes)
                {
                    Console.WriteLine($"Старое объявление. Минуты: {minutes}");
                    continue;
                }

                var nameElement = element.GetElementsByClassName("Link ListingItemTitle-module__link").FirstOrDefault();
                var engineElement = element.GetElementsByClassName("ListingItemTechSummaryDesktop__cell").ToArray();
                var priceElement = element.GetElementsByClassName("ListingItemPrice-module__content").FirstOrDefault();
                var yearElement = element.GetElementsByClassName("ListingItem-module__year").FirstOrDefault();
                var kmAgeElement = element.GetElementsByClassName("ListingItem-module__kmAge").FirstOrDefault();

                var advertisement = new Advertisement
                {
                    Name = nameElement.TextContent,
                    Ref = nameElement.GetAttribute("href"),
                    Price = priceElement.TextContent,
                    Year = yearElement.TextContent,
                    KmAge = kmAgeElement.TextContent,
                    Date = dateElement.TextContent,
                    Engine = engineElement[0].TextContent,
                    Box = engineElement[1].TextContent,
                    Bodywork = engineElement[2].TextContent,
                    Drive = engineElement[3].TextContent,
                    Color = engineElement[4].TextContent,
                    Minutes = minutes
                };

                Console.WriteLine($"Добавили объявление: {advertisement}");

                result.Add(advertisement);
            }

            if (result.Count > 0)
            {
                _manager.SendAdvertisement(result.OrderBy(x => x.Minutes).ToArray());
            }
            else
            {
                Console.WriteLine($"Сори, ничего не найдено {DateTime.Now}");
            }

            return Task.CompletedTask;
        }
    }
}
