using Code.DataClasses;
using TMPro;
using UnityEngine;

namespace Code.GameCore.ObjectsView {

    public class PlayerView : MonoBehaviour {

        [Header("Text Fields")]
        [SerializeField] private TMP_InputField _userIdInput;
        [Space(10)]
        [SerializeField] private TMP_InputField _displayInput;
        [Space(10)]
        [SerializeField] private TMP_InputField _levelInput;

        public string GetUserId() => _userIdInput.text;

        public PlayerData CreatePlayerData() {

            return new PlayerData() {
                displayName = _displayInput.text,
                level = int.Parse(_levelInput.text)
            };
        }

        public void UpdateView(string userID, PlayerData data = null) {

            UpdateTextField(_userIdInput, userID);

            var hasData = data != null;

            UpdateTextField(_displayInput, hasData ? data.displayName : "");

            UpdateTextField(_levelInput, hasData ? data.level.ToString() : "");
        }

        private void UpdateTextField(TMP_InputField text, string value) {
            
            if (text == null) {
                return;
            }

            text.text = value;
        }
    }
}