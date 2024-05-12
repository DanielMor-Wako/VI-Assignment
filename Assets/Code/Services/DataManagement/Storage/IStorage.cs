using System.Threading.Tasks;

namespace Code.Services.DataManagement.Storage {

    public interface IStorage {
        Task SaveAsync(string groupId, string id, string data);
        Task<string> LoadAsync(string groupId, string id, string key);
    }
}