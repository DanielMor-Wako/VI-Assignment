using Code.ScriptableObjectData;
using Code.Services.DataManagement.Serializers;
using Code.Services.DataManagement.Storage;
using UnityEngine;

namespace Code.GameCore {
    public class GameManager : MonoBehaviour {

        [SerializeField] private PrefabBank _prefabBank;

        private GameStateManager _gameStateManager;

        private void Start() 
        {
            var serializer = new JsonSerializer();
            var storage = new FirebaseStorage();

            _gameStateManager = new GameStateManager(storage, serializer);

            LoadGameState();
        }

        [ContextMenu("Load Game State")]
        public void LoadGameState()
        {
            _gameStateManager.LoadGameState();
        }

        [ContextMenu("Save Game State")]
        public void SaveGameState()
        {
            _gameStateManager.SaveGameState();
        }
    }
}
