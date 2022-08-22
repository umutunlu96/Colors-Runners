using System;
using Enums;
using Signals;
using UnityEngine;

namespace Umut
{
    public class UmutBuildingManager : MonoBehaviour
    {
        #region SelfVariables
        [SerializeField] private StructureScriptableObject structure;
        [SerializeField] private UmutBuildingMeshController meshController;
        [SerializeField] private UmutBuildingPhysicsController physicController;
        [SerializeField] private UmutBuildingTextController textController;

        private string _buildingName;
        #endregion

        private void Awake()
        {
            _buildingName = gameObject.name;
            // GetData();
            SetDataToControllers();
        }

        // private void GetData() => structure = Resources.Load<StructureScriptableObject>("Data/" + gameObject.name);

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            IdleSignals.Instance.onPlayerEnterBuildingArea += OnPlayerEnterBuildingArea;
        }
        
        private void UnSubscribeEvents()
        {
            IdleSignals.Instance.onPlayerEnterBuildingArea -= OnPlayerEnterBuildingArea;
        }
        #endregion
        
        
        private void SetDataToControllers()
        {
            meshController.SetData(structure.MainComplateState,structure.SideComplateState);
            physicController.SetData(structure.MainComplateState,structure.SideComplateState,structure.SideBuildingUnlockState);
            
            textController.SetData(structure.MainComplateState,structure.SideComplateState,structure.SideBuildingUnlockState,
                structure.MainName, structure.SideName,structure.MainPayedAmount,structure.MainPrice,
                structure.SidePayedAmount ,structure.SidePrice);
        }

        private void OnPlayerEnterBuildingArea(string nameOfBuilding, string nameOfType)
        {
            if (_buildingName.Equals(nameOfBuilding))
            {
                print("Player Entered " + _buildingName + " Area");
                textController.UpdatePayedAmount(nameOfType);
            }
        }
        
        // private void OnPlayerExitBuildingArea(string nameOfBuilding, string nameOfType)
        // {
        //     if (_buildingName.Equals(nameOfBuilding))
        //     {
        //         print("Player Exited " + _buildingName + " Area");
        //         textController.UpdatePayedAmount(nameOfType);
        //     }
        // }

        public void CheckAreaState(string buildingName)
        {
            if (buildingName.Equals("Main"))
            {
                structure.MainComplateState = BuildingComplateState.Completed;
                structure.SideBuildingUnlockState = BuildingUnlockState.Unlocked;
                //SetData
                textController.SetData(structure.MainComplateState,structure.SideComplateState,structure.SideBuildingUnlockState);
                meshController.SetData(structure.MainComplateState,structure.SideComplateState);
                physicController.SetData(structure.MainComplateState,structure.SideComplateState,structure.SideBuildingUnlockState);
                //CheckData
                textController.CheckData();
                physicController.CheckData();
                meshController.CheckData();
                print(structure.MainComplateState);
            }
            else if (buildingName.Equals("Side"))
            {
                structure.SideComplateState = BuildingComplateState.Completed;
                //SetData
                textController.SetData(structure.MainComplateState,structure.SideComplateState,structure.SideBuildingUnlockState);
                meshController.SetData(structure.MainComplateState,structure.SideComplateState);
                physicController.SetData(structure.MainComplateState,structure.SideComplateState,structure.SideBuildingUnlockState);
                //CheckData
                textController.CheckData();
                physicController.CheckData();
                meshController.CheckData();
                print(structure.SideComplateState);
            }
        }
    }
}