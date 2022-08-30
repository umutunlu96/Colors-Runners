using UnityEngine;
using Data.ValueObject;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_WaypointData", menuName = "ColorsRunners/WaypointDatas", order = 0)]
    public class CD_WaypointData : ScriptableObject
    {
        public WayPointData waypoint;
    }
}