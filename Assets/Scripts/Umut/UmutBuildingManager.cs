using Enums;
using Signals;
using UnityEngine;
using UnityObject;

namespace Umut
{
    public class UmutBuildingManager : MonoBehaviour
    {
        #region SelfVariables
        [SerializeField] private CD_Structure cdStructure;
        [SerializeField] private UmutBuildingMeshController meshController;
        [SerializeField] private UmutBuildingPhysicsController physicController;
        [SerializeField] private UmutBuildingTextController textController;

        private string _buildingName;
        #endregion

        private void Awake()
        {
            _buildingName = gameObject.name;
            // GetData();
            // SetDataToControllers();
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
        
        
        // private void SetDataToControllers()
        // {
        //     meshController.SetData(cdStructure.MainComplateState,cdStructure.SideComplateState);
        //     physicController.SetData(cdStructure.MainComplateState,cdStructure.SideComplateState,cdStructure.SideBuildingUnlockState);
        //     
        //     textController.SetData(cdStructure.MainComplateState,cdStructure.SideComplateState,cdStructure.SideBuildingUnlockState,
        //         cdStructure.MainName, cdStructure.SideName,cdStructure.MainPayedAmount,cdStructure.MainPrice,
        //         cdStructure.SidePayedAmount ,cdStructure.SidePrice);
        // }

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

        // public void CheckAreaState(string buildingName)
        // {
        //     if (buildingName.Equals("Main"))
        //     {
        //         cdStructure.MainComplateState = BuildingComplateState.Completed;
        //         cdStructure.SideBuildingUnlockState = BuildingUnlockState.Unlocked;
        //         //SetData
        //         textController.SetData(cdStructure.MainComplateState,cdStructure.SideComplateState,cdStructure.SideBuildingUnlockState);
        //         meshController.SetData(cdStructure.MainComplateState,cdStructure.SideComplateState);
        //         physicController.SetData(cdStructure.MainComplateState,cdStructure.SideComplateState,cdStructure.SideBuildingUnlockState);
        //         //CheckData
        //         textController.CheckData();
        //         physicController.CheckData();
        //         meshController.CheckData();
        //         print(cdStructure.MainComplateState);
        //     }
        //     else if (buildingName.Equals("Side"))
        //     {
        //         cdStructure.SideComplateState = BuildingComplateState.Completed;
        //         //SetData
        //         textController.SetData(cdStructure.MainComplateState,cdStructure.SideComplateState,cdStructure.SideBuildingUnlockState);
        //         meshController.SetData(cdStructure.MainComplateState,cdStructure.SideComplateState);
        //         physicController.SetData(cdStructure.MainComplateState,cdStructure.SideComplateState,cdStructure.SideBuildingUnlockState);
        //         //CheckData
        //         textController.CheckData();
        //         physicController.CheckData();
        //         meshController.CheckData();
        //         print(cdStructure.SideComplateState);
        //     }
        // }
    }
}