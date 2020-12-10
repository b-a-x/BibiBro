using System.Threading.Tasks;
using BibiBro.Client.Telegram.Collections;
using BibiBro.Client.Telegram.Parser;

namespace BibiBro.Client.Telegram
{
    public interface ITelegramService
    {
        Task StartAsync();
        Task StopAsync();
    }

    public class TelegramService : ITelegramService
    {
        private readonly ITelegramManager _telegramManager;
        private readonly IChatCollection _chatCollection;

        public TelegramService(ITelegramManager telegramManager, IChatCollection chatCollection)
        {
            _telegramManager = telegramManager;
            _chatCollection = chatCollection;
        }

        public async Task StartAsync()
        {
            await _chatCollection.InitializeAsync();
            await _telegramManager.Start();
            //await ParserAutoRuScheduler.StartAsync();
        }

        public async Task StopAsync()
        {
            await _telegramManager.Stop();
        }
    }
}