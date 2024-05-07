using System.Threading.Tasks;

public interface IStorage {
    Task SaveAsync(string key, string data);
    Task<string> LoadAsync(string key);
}