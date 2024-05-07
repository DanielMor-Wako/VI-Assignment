using System.Threading.Tasks;

namespace Code.Services.DataManagement.Storage {
    public class FirebaseStorage : IStorage {

        public Task<string> LoadAsync(string key) {
            // Todo: Load from storage
            return null;
        }

        public Task SaveAsync(string key, string data) {
            // Todo: Save to storage
            return Task.CompletedTask;
        }
    }
}