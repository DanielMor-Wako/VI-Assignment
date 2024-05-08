using Code.ScriptableObjectData;
using Code.Services.DataManagement.Serializers;
using Code.Services.DataManagement.Storage;
using UnityEngine;

namespace Code.GameCore {

    public class GameManager : MonoBehaviour {

        [SerializeField] private PrefabBank _prefabBank;

        private GameStateManager _gameStateManager;


        [ContextMenu("Load Game State")]
        public void LoadGameState() {

            _gameStateManager.LoadGameState();
        }

        [ContextMenu("Save Game State")]
        public void SaveGameState() {

            _gameStateManager.SaveGameState();
        }

        private void Start() {

            var serializer = new JsonSerializer();
            var storage = new FirebaseStorage();

            GetPrefabBank();

            _gameStateManager = new GameStateManager(storage, serializer);

            LoadGameState();
        }

        private void GetPrefabBank() {

            _prefabBank ??= Resources.Load<PrefabBank>("PrefabBank/PrefabBank");

            if (_prefabBank == null) {
                Debug.LogError("PrefabBank not found in Resources folder. Please check that the PrefabBank is located at Assets/Resources/PrefabBank/PrefabBank.asset");
            }
        }
    }
}
