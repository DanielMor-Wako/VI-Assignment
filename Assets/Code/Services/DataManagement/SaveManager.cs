using Code.DataClasses;
using Code.Services.DataManagement.Serializers;
using Code.Services.DataManagement.Storage;
using System.Threading.Tasks;

namespace Code.Services.DataManagement {

    public class SaveManager {

        private IStorage _storage;
        private ISerializer _serializer;

        public SaveManager(IStorage storage, ISerializer serialization) {
            _storage = storage;
            _serializer = serialization;
        }

        public async Task SaveAsync(string userId, GameStateData data) {
            string serializedData = _serializer.Serialize(data);
            await _storage.SaveAsync(userId, serializedData);
        }

        public async Task<GameStateData> LoadAsync(string userId, string key) {
            var serializedData = await _storage.LoadAsync(userId, key);
            return _serializer.Deserialize<GameStateData>(serializedData);
        }
    }
}