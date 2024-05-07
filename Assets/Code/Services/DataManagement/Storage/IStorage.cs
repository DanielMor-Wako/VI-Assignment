using System.Threading.Tasks;

namespace Code.Services.DataManagement.Storage {

    public interface IStorage {
        Task SaveAsync(string data);
        Task<string> LoadAsync(string key);
    }
}