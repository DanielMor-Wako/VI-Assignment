using System.Threading.Tasks;
using Firebase.Database;

namespace Code.Services.DataManagement.Storage {
    public class FirebaseStorage : IStorage {

        private DatabaseReference _dbReference;

        private const string UrlPath = "vi-assignment-default-rtdb.firebaseio.com";

        public FirebaseStorage() {
            _dbReference = FirebaseDatabase.GetInstance(UrlPath).RootReference;
        }

        public async Task<string> LoadAsync(string id, string key) {

            var result = string.Empty;

            await _dbReference.Child("users").Child(id).GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted) {
                    return;
                }
                if (task.IsCompleted) {
                    DataSnapshot snapShot = task.Result;
                    result = snapShot.GetRawJsonValue();
                }
            });

            return result;
        }

        public async Task SaveAsync(string id, string data) {

            await _dbReference.Child("users").Child(id).SetRawJsonValueAsync(data).ContinueWith(task => {
                if (task.IsFaulted) {
                    UnityEngine.Debug.LogError("Saving Failed");
                    return;
                }
                if (task.IsCompleted) {
                    UnityEngine.Debug.Log("Saving Complete");
                    return;
                }
            });
        }

    }
}