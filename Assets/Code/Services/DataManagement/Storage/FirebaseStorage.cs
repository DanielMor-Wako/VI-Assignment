using Code.DataClasses;
using System.Threading.Tasks;

namespace Code.Services.DataManagement.Storage {
    public class FirebaseStorage : IStorage {

        public async Task<string> LoadAsync(string key) {

            string playerData = "{\"displayName\":\"Shalom\", \"level\":10}";
            string tempString = "{\"objects\":[{\"key\":\"playerData\", \"value\":\"" + playerData.Replace("\"", "\\\"") + "\"}]}";

            return tempString;
            
            // Todo: Load from storage
        }

        public async Task SaveAsync(string data) {

            
            // Todo: Save to storage
        }

    }
}