using System.Collections.Generic;
using UnityEngine;

namespace Umut
{
    [CreateAssetMenu(fileName = "UmutDeneme", menuName = "UmutDeneme/City", order = 0)]
    public class CD_CityScriptableObject : ScriptableObject
    {
        public List<StructureScriptableObject> CityScriptableObject;
    }
}