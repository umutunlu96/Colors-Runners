using System;
using Enums;
using UnityEngine;

namespace Umut
{
    [Serializable]
    [CreateAssetMenu(fileName = "UmutDeneme", menuName = "UmutDeneme/Building", order = 0)]
    public class StructureScriptableObject : ScriptableObject
    {
        [Header("BuildingType")]
        public BuildType BuildingType;

        [Space]
        [Header("Main")]
        public string MainName;
        public BuildingUnlockState MainBuildingUnlockState = BuildingUnlockState.Unlocked;
        public BuildingComplateState MainComplateState = BuildingComplateState.Uncompleted;
        public Color32 MainColor;
        public int MainPrice;
        public int MainPayedAmount;
        public int MainPayedAmountText;

        [Header("Side")]
        public string SideName;
        public BuildingUnlockState SideBuildingUnlockState = BuildingUnlockState.Locked;
        public BuildingComplateState SideComplateState = BuildingComplateState.Uncompleted;
        public Color32 SideColor;
        public int SidePrice;
        public int SidePayedAmount;
        public int SidePayedAmountText;
    }
}