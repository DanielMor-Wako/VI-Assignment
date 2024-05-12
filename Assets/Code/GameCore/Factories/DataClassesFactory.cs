using Code.Services.DataManagement.Serializers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Code.GameCore.Factories {

    public class DataClassesFactory {

        private static Dictionary<string, Type> _registeredInstances;
        private ISerializer _serializer;

        public DataClassesFactory(ISerializer serialization) {

            _serializer = serialization;

            _registeredInstances = new Dictionary<string, Type>();
        }

        public IEnumerable<string> GetRegisteredKeys() => _registeredInstances.Keys;

        public void Register(string key, Type type) {

            _registeredInstances[key] = type;
        }

        public object Create(string key, string data) {

            if (_registeredInstances.ContainsKey(key)) {

                Type objectType = _registeredInstances[key];

                MethodInfo deserializeMethod = _serializer.GetType().GetMethod("Deserialize").MakeGenericMethod(objectType);
                object deserializedData = deserializeMethod.Invoke(_serializer, new object[] { data });

                return deserializedData;
            } else {

                throw new KeyNotFoundException($"The key '{key}' does not exist in the registered instance creation methods.");
            }

        }

    }
}