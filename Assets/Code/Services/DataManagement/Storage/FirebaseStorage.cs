using System.Threading.Tasks;
using Firebase.Database;

namespace Code.Services.DataManagement.Storage {
    public class FirebaseStorage : IStorage {

        private DatabaseReference _dbReference;

        public FirebaseStorage() {
            _dbReference = FirebaseDatabase.GetInstance("vi-assignment-default-rtdb.firebaseio.com").RootReference;
        }

        public async Task<string> LoadAsync(string key) {

            var result = string.Empty;

            await _dbReference.Child("users").Child("testuser").GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted) {
                    return;
                }
                if (task.IsCompleted) {
                    DataSnapshot snapShot = task.Result;
                    result = snapShot.GetRawJsonValue();
                }
            });

            return result;

            // Load from storage mock
            string playerData = "{\"displayName\":\"Shalom\", \"level\":10}";
            string tempString = "{\"objects\":[{\"key\":\"playerData\", \"value\":\"" + playerData.Replace("\"", "\\\"") + "\"}]}";
            return tempString;
        }

        public async Task SaveAsync(string data) {

            // Save to storage
            UnityEngine.Debug.Log("Started saving");
            await _dbReference.Child("users").Child("testuser").SetRawJsonValueAsync(data).ContinueWith(task => {
                if (task.IsFaulted) {
                    return;
                }
                if (task.IsCompleted) {
                    return;
                }
            });
            UnityEngine.Debug.Log("Saving complete");
        }

    }
}