using Code.DataClasses;
using Code.Services.DataManagement.Serializers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Code.GameCore.Factories {

    public class GameObjectFactory {

        private static Dictionary<string, Type> _instanceCreationMethods;
        private ISerializer _serializer;

        public GameObjectFactory(ISerializer serialization) {

            _serializer = serialization;

            _instanceCreationMethods = new Dictionary<string, Type>();
        }

        public void Register(string key, Type type) {

            _instanceCreationMethods[key] = type;
        }

        public object Create(string key, string data) {

            if (_instanceCreationMethods.ContainsKey(key)) {

                Type objectType = _instanceCreationMethods[key];

                MethodInfo deserializeMethod = _serializer.GetType().GetMethod("Deserialize").MakeGenericMethod(objectType);
                object deserializedData = deserializeMethod.Invoke(_serializer, new object[] { data });

                return deserializedData;
            } else {

                throw new KeyNotFoundException($"The key '{key}' does not exist in the registered instance creation methods.");
            }

        }

        public IEnumerable<string> GetRegisteredKeys() {
            return _instanceCreationMethods.Keys;
        }
    }
}