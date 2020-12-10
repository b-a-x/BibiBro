using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BibiBro.Client.Telegram.Helper
{
    public interface IReadWriteJson
    {
        string Path { get; set; }
        Task AddAsync<T>(T item) where T : class;
        Task RemoveAsync<T>(T item) where T : class;
        Task<List<T>> ReadAsync<T>() where T : class;
    }

    public class ReadWriteJson : IReadWriteJson
    {
        public string Path { get; set; }

        public async Task AddAsync<T>(T item) where T : class
        {
            if(item == null)
                return;

            var collection = await ReadAsync<T>();
            if (collection.Contains(item) == false)
            {
                collection.Add(item);
                await WriteAsync(collection);
            }
        }

        public async Task RemoveAsync<T>(T item) where T : class
        {
            if (item == null)
                return;

            var collection = await ReadAsync<T>();
            if (collection.Contains(item))
            {
                collection.Add(item);
                await WriteAsync(collection);
            }
        }

        public async Task<List<T>> ReadAsync<T>() where T : class
        {
            var text = await File.ReadAllTextAsync(Path);
            return JsonSerializer.Deserialize<List<T>>(text);
        }

        private async Task WriteAsync<T>(IReadOnlyCollection<T> collection) where T : class
        {
            await File.WriteAllTextAsync(Path,JsonSerializer.Serialize(collection));
        }
    }
}