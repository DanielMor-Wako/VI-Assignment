using System;
using System.Threading.Tasks;

public class SaveManager {
    private IStorage _storage;
    private ISerializer _serializer;

    public SaveManager(IStorage storage, ISerializer serialization) {
        _storage = storage;
        _serializer = serialization;
    }
}