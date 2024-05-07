using Code.Services.DataManagement.Serializers;
using Code.Services.DataManagement.Storage;
using UnityEngine;

namespace Code.GameCore {
    public class GameManager : MonoBehaviour {

        private GameStateManager _gameStateManager;

        private void Start() {

            var serializer = new JsonSerializer();
            var storage = new FirebaseStorage();

            _gameStateManager = new GameStateManager(storage, serializer);
        }

    }
}
