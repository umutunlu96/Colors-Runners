using Controllers;
using Enums;
using UnityEngine;
using UnityObject;
using ValueObject;

namespace Managers
{
    public class BuildingManager : MonoBehaviour
    {
        [Header("Building Datas")]
        private EnvironmentData _environmentData;

        public BuildingData _mainBuilding;
        public BuildingData _SideBuilding;
        public BuildType buildingType;

        [Space]
        [Header("Main building Script References")]
        [SerializeField] private BuildingMeshController buildingMeshcontroller;

        [SerializeField] private BuildingPhysicController buildingPhysiccontroller;
        [SerializeField] private BuildingScoreController buildingScoreController;

        private void Awake()
        {
            GetEnvironmentData((int)buildingType);
            SetBuildings();
            SendDataToController();
        }

        private void GetEnvironmentData(int building) => _environmentData = Resources.Load<CD_EnvironmentData>("Data/CD_EnvironmentData").EnvironmentData[building];

        private void CheckEnvironmentData()
        {
            switch (_mainBuilding.ComplateState)
            {
                case BuildingComplateState.Uncompleted:
                    buildingMeshcontroller = transform.GetChild(0).GetComponentInChildren<BuildingMeshController>();
                    buildingPhysiccontroller = transform.GetChild(0).GetComponentInChildren<BuildingPhysicController>();
                    buildingScoreController = transform.GetChild(0).GetComponentInChildren<BuildingScoreController>();
                    _SideBuilding.BuildingUnlockState = BuildingUnlockState.Locked;
                    break;

                case BuildingComplateState.Completed:
                    buildingMeshcontroller = transform.GetChild(1).GetComponentInChildren<BuildingMeshController>();
                    buildingPhysiccontroller = transform.GetChild(1).GetComponentInChildren<BuildingPhysicController>();
                    buildingScoreController = transform.GetChild(1).GetComponentInChildren<BuildingScoreController>();
                    _SideBuilding.BuildingUnlockState = BuildingUnlockState.Unlocked;
                    break;
            }
        }

        private void SendDataToController()
        {
        }

        public void PlayerEnterTriggerArea()
        {
        }

        private void SetBuildings()
        {
            _mainBuilding = _environmentData.Buildings[0];
            _SideBuilding = _environmentData.Buildings[1];
        }
    }
}