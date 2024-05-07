using System.Threading.Tasks;

public class SaveManager {

    private IStorage _storage;
    private ISerializer _serializer;

    public SaveManager(IStorage storage, ISerializer serialization) {
        _storage = storage;
        _serializer = serialization;
    }

    public async Task SaveAsync<T>(string key, T data) {
        string serializedData = _serializer.Serialize(data);
        await _storage.SaveAsync(key, serializedData);
    }

    public async Task<T> LoadAsync<T>(string key) {
        string serializedData = await _storage.LoadAsync(key);
        return _serializer.Deserialize<T>(serializedData);
    }
}