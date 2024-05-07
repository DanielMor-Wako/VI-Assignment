using Code.Services.DataManagement.Serializers;
using Code.Services.DataManagement.Storage;
using Code.Services.DataManagement;
using System.Collections.Generic;
using UnityEngine;
using Code.DataClasses;

namespace Code.GameCore {

    public class GameStateManager {

        private ISerializer _serializer;
        private IStorage _storage;

        private SaveManager _saveManager;
        private GameObjectFactory _instanceFactory;
        private Dictionary<string, object> _serializedInstances;

        public GameStateManager(IStorage storage, ISerializer serialization) {

            _storage = storage;
            _serializer = serialization;

            _saveManager = new SaveManager(_storage, _serializer);

            _instanceFactory = new GameObjectFactory(_serializer);

            LoadGameState();
        }

        private async void LoadGameState() {

            _serializedInstances = new Dictionary<string, object>();
            var gameState = await _saveManager.LoadAsync("gameState");
            foreach (var obj in gameState.objects) {
                Debug.Log($"value for {obj.key} is {obj.value}");
                var instance = _instanceFactory.Create(obj.key, obj.value);
                // todo: maybe change the obj.key to uniqueIdentifier when object created
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

        public async void SaveGameState() {

            var newGameState = new GameStateData();
            newGameState.objects = new();

            foreach (var (kvp, vvp) in _serializedInstances) {

                var serializeMethod = _serializer.GetType().GetMethod("Serialize").MakeGenericMethod(vvp.GetType());

                string serializedData = (string) serializeMethod.Invoke(_serializer, new object[] { vvp });

                newGameState.objects.Add(new ObjectRecord() { key = kvp, value = serializedData });
            }


            await _saveManager.SaveAsync(newGameState);
        }

    }
}
