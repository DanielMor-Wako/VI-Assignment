using Code.Services.DataManagement.Serializers;
using Code.Services.DataManagement.Storage;
using Code.Services.DataManagement;
using System.Collections.Generic;
using UnityEngine;
using Code.DataClasses;
using System.Threading.Tasks;

namespace Code.GameCore {

    public class GameStateManager {

        private ISerializer _serializer;
        private IStorage _storage;

        private SaveManager _saveManager;
        private GameObjectFactory _instanceFactory;
        private Dictionary<string, object> _serializedInstances = new();

        public GameStateManager(IStorage storage, ISerializer serialization) {

            _storage = storage;
            _serializer = serialization;

            _saveManager = new SaveManager(_storage, _serializer);

            _instanceFactory = new GameObjectFactory(_serializer);
        }

        public Dictionary<string, object> GetSerializedInstances() => _serializedInstances;

        public async Task SaveGameState() {

            var newGameState = new GameStateData();
            newGameState.objects = new();

            _serializedInstances = new Dictionary<string, object>();
            _serializedInstances.Add("playerData", new PlayerData() { displayName = "testuser", level = 0 });

            foreach (var (kvp, vvp) in _serializedInstances) {

                var serializeMethod = _serializer.GetType().GetMethod("Serialize").MakeGenericMethod(vvp.GetType());

                string serializedData = (string)serializeMethod.Invoke(_serializer, new object[] { vvp });

                newGameState.objects.Add(new ObjectRecord() { key = kvp, value = serializedData });
            }


            await _saveManager.SaveAsync(newGameState);
        }

        public async Task LoadGameState() {

            _serializedInstances = new();
            var gameState = await _saveManager.LoadAsync("gameState");

            if (gameState == null) {
                return;
            }

            foreach (var obj in gameState.objects) {
                Debug.Log($"value for {obj.key} is {obj.value}");

                // todo: maybe change the obj.key to uniqueIdentifier when object created
                var instance = _instanceFactory.Create(obj.key, obj.value);
                _serializedInstances.Add(obj.key, instance);
            }

            ApplyGameState();
        }

        private void ApplyGameState() {

            foreach (var (key, obj) in _serializedInstances) {
                var go = obj as GameObject;
                if (go == null) {
                    continue;
                }
                Debug.Log($"Applied new state for : {obj.ToString()}");
            }
        }

    }
}
