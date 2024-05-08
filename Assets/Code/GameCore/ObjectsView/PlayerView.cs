using Code.DataClasses;
using TMPro;
using UnityEngine;

namespace Code.GameCore.ObjectsView {

    public class PlayerView : MonoBehaviour {

        [Header("Text Fields")]
        [SerializeField] private TMP_InputField _userIdInput;
        [SerializeField] private TMP_Text _userIdText;
        [Space(10)]
        [SerializeField] private TMP_InputField _displayInput;
        [SerializeField] private TMP_Text _displayText;
        [Space(10)]
        [SerializeField] private TMP_InputField _levelInput;
        [SerializeField] private TMP_Text _levelText;

        public PlayerData GetPlayerData() {

            return new PlayerData() {
                displayName = _displayInput.text,
                level = int.Parse(_levelInput.text)
            };
        }

        public void UpdateView(string userID, PlayerData data = null) {

            UpdateTextField(_userIdText, userID);

            var hasData = data != null;

            UpdateTextField(_displayText, hasData ? data.displayName : "");

            UpdateTextField(_levelText, hasData ? data.level.ToString() : "0");
        }

        private void UpdateTextField(TMP_Text text, string data) {
            
            if (text == null) {
                return;
            }

            text.SetText(data);
        }
    }
}