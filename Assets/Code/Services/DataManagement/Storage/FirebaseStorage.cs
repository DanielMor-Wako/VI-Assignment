using System.Threading.Tasks;
using Firebase;
using Firebase.Database;

namespace Code.Services.DataManagement.Storage {
    public class FirebaseStorage : IStorage {

        private DatabaseReference _dbReference;

        public FirebaseStorage()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                try {
                    if (task.Result == DependencyStatus.Available) {

                        _dbReference = FirebaseDatabase.DefaultInstance.RootReference;
                        // ("Firebase Realtime Database URL: " + _dbReference.DatabaseUrl.ToString());
                    } else {
                        // ("Could not resolve all Firebase dependencies: " + task.Result);
                    }
                }
                catch (System.Exception) {
                    throw new FirebaseException(0, "Error checking Firebase dependencies");
                }
            });
        }

        public async Task<string> LoadAsync(string groupid, string id) {

            var result = string.Empty;

            await _dbReference.Child(groupid).Child(id).GetValueAsync().ContinueWith(task => {
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

        public async Task SaveAsync(string groupid, string id, string data) {

            await _dbReference.Child(groupid).Child(id).SetRawJsonValueAsync(data).ContinueWith(task => {
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