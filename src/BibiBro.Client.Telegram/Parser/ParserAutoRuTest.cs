using System;
using System.Net.Http;
using System.Threading.Tasks;
using BibiBro.Client.Telegram.Model;
using BibiBro.Client.Telegram.Services;

namespace BibiBro.Client.Telegram.Parser
{
    public class ParserAutoRuTest : IParserAutoRu
    {
        private readonly IAutoRuService _service;
       
        public ParserAutoRuTest(IAutoRuService service)
        {
            _service = service;
        }

        public async Task Pars()
        {
            AutoRuData result = new AutoRuData { Status = "Test"};
            try
            {
               result = await _service.GetData();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            Console.WriteLine($"Успех {result}");
            /*HttpClient client = new HttpClient();

            HttpResponseMessage responce;
            try
            {
                responce = client.PostAsync("https://auto.ru/-/ajax/desktop/listing/", new ByteArrayContent(null)).Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.CompletedTask;
            }

            if (responce.IsSuccessStatusCode == false)
            {
                Console.WriteLine($"Запрос выполнился с ошибкой {DateTime.Now}");
                Console.WriteLine(responce.RequestMessage.Content.ReadAsStringAsync().Result);
                return Task.CompletedTask;
            }

            var source = responce.Content.ReadAsStringAsync().Result;
            */
        }
    }
}
