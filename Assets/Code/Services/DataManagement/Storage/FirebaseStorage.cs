using Code.DataClasses;
using System.Threading.Tasks;

namespace Code.Services.DataManagement.Storage {
    public class FirebaseStorage : IStorage {

        public async Task<string> LoadAsync(string key) {

            var tempString = "{\"objects\":[{\"key\":\"playerData\", {\"value\":\"playerData\"}]}";
            return tempString;
            
            // Todo: Load from storage
            var objectRecord = new ObjectRecord() { key = "playerData", value = "" };
        }

        public async Task SaveAsync(string data) {

            
            // Todo: Save to storage
        }

    }
}