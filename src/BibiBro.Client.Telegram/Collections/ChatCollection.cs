using System.Collections.Generic;
using System.Threading.Tasks;
using BibiBro.Client.Telegram.Helper;
using BibiBro.Client.Telegram.Model;

namespace BibiBro.Client.Telegram.Collections
{
    public interface IChatCollection
    {
        IReadOnlyCollection<Chat> Collection { get; }
        Task AddAsync(Chat item);
        Task RemoveAsync(Chat item);
        Task InitializeAsync();
    }

    public class ChatCollection : IChatCollection
    {
        private readonly IReadWriteJson _readWriteJson;
        private readonly List<Chat> _chat = new();

        public ChatCollection(IReadWriteJson readWriteJson)
        {
            _readWriteJson = readWriteJson;
            _readWriteJson.Path = "Data/ChatCollection.json";
        }

        public IReadOnlyCollection<Chat> Collection => _chat;

        public async Task AddAsync(Chat item)
        {
            if (_chat.Contains(item) == false)
            {
                _chat.Add(item);
                await _readWriteJson.AddAsync(item);
            }
        }

        public async Task RemoveAsync(Chat item)
        {
            if (_chat.Contains(item))
            {
                _chat.Remove(item);
                await _readWriteJson.RemoveAsync(item);
            }
        }

        public async Task InitializeAsync()
        {
            _chat.AddRange(await _readWriteJson.ReadAsync<Chat>());
        }
    }
}
