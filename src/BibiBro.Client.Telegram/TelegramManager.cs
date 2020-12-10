using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibiBro.Client.Telegram.Collections;
using BibiBro.Client.Telegram.Helper;
using BibiBro.Client.Telegram.Model;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace BibiBro.Client.Telegram
{
    public interface ITelegramManager
    {
        Task Start();
        Task Stop();
        void SendAdvertisement(IReadOnlyCollection<Advertisement> data);
    }

    public class TelegramManager : ITelegramManager
    {
        private readonly List<Advertisement> _cache = new List<Advertisement>();
        private readonly IChatCollection _chatCollection;
        private readonly TelegramBotClient _botClient = new TelegramBotClient("1456775506:AAF4QyFpdog4GCn53b8tXJeIwKkcIZTRKhg");

        public TelegramManager(IChatCollection chatCollection) : base()
        {
            _chatCollection = chatCollection;
        }

        public Task Start()
        {
            Initialize();
            _botClient.StartReceiving();
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            _botClient.StartReceiving();
            return Task.CompletedTask;
        }

        private Task Initialize()
        {
            _botClient.OnMessage += OnMessage;
            return Task.CompletedTask;
        }

        private async void OnMessage(object sender, MessageEventArgs e)
        {
            
            var message = e.Message;
            if (message != null && message.Type == MessageType.Text)
            {
                switch (message.Text)
                {
                    case BibiBroTelegramCommand.Start:
                        await _botClient.SendTextMessageAsync(message.From.Id, BibiBroTelegramMessage.Start);
                        break;
                    case BibiBroTelegramCommand.Search:
                        await _chatCollection.AddAsync(new Chat(message.Chat.Id));
                        break;
                    case BibiBroTelegramCommand.StopSearch:
                        await _chatCollection.RemoveAsync(new Chat(message.Chat.Id));
                        break;
                }
            }
        }

        public void SendAdvertisement(IReadOnlyCollection<Advertisement> data)
        {
            if (_cache.Count <= 0)
            {
                _cache.AddRange(data);
                SendAdvertisementText(_cache);
                Console.WriteLine($"Объявления новые нашли {DateTime.Now}");
            }
            else
            {
                var temp = data.Except(_cache).ToArray();
                if (temp.Length > 0)
                {
                    _cache.Clear();
                    _cache.AddRange(data);
                    SendAdvertisementText(temp);
                    Console.WriteLine($"Объявления новые нашли {DateTime.Now}");
                }
                else
                {
                    Console.WriteLine($"Объявлений новых нет {DateTime.Now}");
                }
            }
        }

        private void SendAdvertisementText(IReadOnlyCollection<Advertisement> data)
        {
            foreach (var chat in _chatCollection.Collection)
            {
                foreach (var item in data)
                {
                    _botClient.SendTextMessageAsync(chat.Id, item.ToString());
                }
            }
        }
    }
}
