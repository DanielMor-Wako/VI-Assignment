using System.Threading.Tasks;

namespace Code.Services.DataManagement.Storage {

    public interface IStorage {
        Task SaveAsync(string key, string data);
        Task<string> LoadAsync(string key);
    }
}