using UnityEngine;

public class JsonSerializer : ISerializer {
    public string Serialize<T>(T data) {
        return JsonUtility.ToJson(data);
    }

    public T Deserialize<T>(string serializedData) {
        return JsonUtility.FromJson<T>(serializedData);
    }
}