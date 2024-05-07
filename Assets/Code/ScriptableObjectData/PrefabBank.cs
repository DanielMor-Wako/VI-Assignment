using System.Collections.Generic;
using UnityEngine;

namespace Code.ScriptableObjectData {

    [CreateAssetMenu(fileName = "PrefabBank", menuName = "Data/PrefabBank", order = 1)]
    public class PrefabBank : ScriptableObject {

        public List<PrefabData> prefabList = new List<PrefabData>();
    }
}
