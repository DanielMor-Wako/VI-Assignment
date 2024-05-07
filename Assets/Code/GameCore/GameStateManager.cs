using Code.Services.DataManagement.Serializers;
using Code.Services.DataManagement.Storage;
using Code.Services.DataManagement;
using System.Collections.Generic;
using UnityEngine;
using Code.DataClasses;

namespace Code.GameCore {

    public class GameStateManager : MonoBehaviour {

        // todo: create GameManager script that init the GameStateManager and determines the serializer and storage
        private JsonSerializer _serializer;
        private SaveManager _saveManager;
        private GameObjectFactory _instanceFactory;
        private Dictionary<string, object> _serializedInstances;

        private void Start() {

            _serializer = new JsonSerializer();
            _saveManager = new SaveManager(new FirebaseStorage(), _serializer);

            _instanceFactory = new GameObjectFactory(_serializer);

            LoadGameState();
        }

        private async void LoadGameState() {

            _serializedInstances = new Dictionary<string, object>();
            var gameState = await _saveManager.LoadAsync("gameState");
            foreach (var obj in gameState.objects) {
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
