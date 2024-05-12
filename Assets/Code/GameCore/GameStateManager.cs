using Code.Services.DataManagement.Serializers;
using Code.Services.DataManagement.Storage;
using Code.Services.DataManagement;
using System.Collections.Generic;
using UnityEngine;
using Code.DataClasses;
using System.Threading.Tasks;
using Code.GameCore.Factories;

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
            _instanceFactory.Register("playerData", typeof(PlayerData));
        }

        public Dictionary<string, object> GetSerializedInstances() => _serializedInstances;

        public async Task SaveGameState(string groupId, string userId) {

            var newGameState = new GameStateData();

            foreach (var (kvp, vvp) in _serializedInstances) {

                var serializeMethod = _serializer.GetType().GetMethod("Serialize").MakeGenericMethod(vvp.GetType());

                string serializedData = (string)serializeMethod.Invoke(_serializer, new object[] { vvp });

                newGameState.objects.Add(new ObjectRecord() { key = kvp, value = serializedData });
            }


            await _saveManager.SaveAsync(groupId, userId, newGameState);
        }

        public async Task LoadGameState(string groupId, string userId) {

            _serializedInstances = new();
            var gameState = await _saveManager.LoadAsync(groupId, userId, "gameState");

            if (gameState == null || gameState.objects.Count == 0) {
                return;
            }

            foreach (var obj in gameState.objects) {

                Debug.Log($"value for {obj.key} is {obj.value}");

                var instance = _instanceFactory.Create(obj.key, obj.value);

                _serializedInstances.Add(obj.key, instance);
            }

        }

    }
}
