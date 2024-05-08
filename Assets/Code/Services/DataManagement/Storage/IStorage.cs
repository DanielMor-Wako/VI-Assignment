using System.Threading.Tasks;

namespace Code.Services.DataManagement.Storage {

    public interface IStorage {
        Task SaveAsync(string id, string data);
        Task<string> LoadAsync(string id, string key);
    }
}