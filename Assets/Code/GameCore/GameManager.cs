using Code.DataClasses;
using Code.GameCore.ObjectsView;
using Code.ScriptableObjectData;
using Code.Services.DataManagement.Serializers;
using Code.Services.DataManagement.Storage;
using UnityEngine;

namespace Code.GameCore {

    public class GameManager : MonoBehaviour {

        [SerializeField] private PrefabBank _prefabBank;

        [Header("Default UserId on start")]
        [SerializeField] private string _userId = "testuser1";

        [Header("View")]
        [SerializeField] private PlayerView _playerView;

        private GameStateManager _gameStateManager;

        [ContextMenu("Load Game State")]
        public async void LoadGameState() {

            _userId = _playerView.GetUserId();

            await _gameStateManager.LoadGameState(_userId);

            var instances = _gameStateManager.GetSerializedInstances();
            if (instances == null) {
                Debug.LogWarning("Instances data could not be found");
                return;
            }

            var playerData = instances["playerData"] as PlayerData;

            _playerView.UpdateView(_userId, playerData);
        }

        [ContextMenu("Save Game State")]
        public async void SaveGameState() {

            var instances = _gameStateManager.GetSerializedInstances();

            instances["playerData"] = _playerView.CreatePlayerData();

            await _gameStateManager.SaveGameState(_userId);
        }

        private void Start() {

            var serializer = new JsonSerializer();
            var storage = new FirebaseStorage();

            GetPrefabBank();

            _gameStateManager = new GameStateManager(storage, serializer);

            _playerView.UpdateView(_userId);
        }

        private void GetPrefabBank() {

            _prefabBank ??= Resources.Load<PrefabBank>("PrefabBank/PrefabBank");

            if (_prefabBank == null) {
                Debug.LogError("PrefabBank not found in Resources folder. Please check that the PrefabBank is located at Assets/Resources/PrefabBank/PrefabBank.asset");
            }
        }
    }
}
