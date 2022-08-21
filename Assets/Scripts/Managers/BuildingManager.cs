using System.Collections;
using Controllers;
using UnityEngine;
using UnityObject;
using Enums;
using ValueObject;

namespace Managers
{
    public class BuildingManager : MonoBehaviour
    {
        [Header("Building Datas")]
        public EnvironmentData _environmentData;
        public BuildingData _mainBuilding;
        public BuildingData _SideBuilding;
        public BuildType buildingType;
        [Space]
        [Header("Main building Script References")]
        [SerializeField] BuildingMeshController mainBuildingMeshcontroller;
        [SerializeField] BuildingPhysicController mainBuildingPhysiccontroller;
        [SerializeField] BuildingScoreController mainBuildingScoreController;
        [Space]
        [Header("Side building Script References")]
        [SerializeField] BuildingMeshController sideBuildingMeshcontroller;
        [SerializeField] BuildingPhysicController sideBuildingPhysiccontroller;
        [SerializeField] BuildingScoreController sideBuildingScoreController;

        private void Awake()
        {
            GetEnvironmentData((int)buildingType);
            SetBuildings();
            SendDataToController();
        }

        private void GetEnvironmentData(int building) => _environmentData = Resources.Load<CD_EnvironmentData>("Data/CD_EnvironmentData").EnvironmentData[building];

        private void SendDataToController()
        {
            mainBuildingScoreController.GetData(_mainBuilding.Name, _mainBuilding.PayedAmount, _mainBuilding.Price);
            sideBuildingScoreController.GetData(_SideBuilding.Name, _SideBuilding.PayedAmount, _SideBuilding.Price);
            
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