using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityObject;

namespace Managers
{
    public class CityManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField] private List<BuildingManager> buildingManagers;
        [SerializeField] private SaveManager saveManager;
        
        #endregion

        #region Private Variables

        private int _currentIdleLevel = 1;
        private int _currentScore = 0;
        [ShowInInspector] private IdleLevelData _levelsData;
        [ShowInInspector] private List<BuildingData> _buildingDatas;

        [ShowInInspector] private List<int> mainPayedAmount = new List<int>();
        [ShowInInspector] private List<BuildingComplateState> mainComplateState = new List<BuildingComplateState>();
        [ShowInInspector] private List<int> sidePayedAmount = new List<int>();
        [ShowInInspector] private List<BuildingComplateState> sideComplateState = new List<BuildingComplateState>();

        #endregion

        #endregion

        private void Awake()
        {
            _levelsData = GetIdleLevelBuildingData();
            GetCurrentLevelData();
            SetDataToBuildingManagers();
        }
        
        private IdleLevelData GetIdleLevelBuildingData() => Resources.Load<CD_IdleLevelData>("Data/CD_IdleLevelData").IdleLevel;

        private int GetIdleLevel() => _currentIdleLevel;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.Instance.onGetIdleLevelId += GetIdleLevel;
            SaveSignals.Instance.onIdleSaveData += OnSaveData;
        }

        private void UnSubscribeEvents()
        {
            SaveSignals.Instance.onGetIdleLevelId -= GetIdleLevel;
            SaveSignals.Instance.onIdleSaveData -= OnSaveData;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion
        
        
        private void GetCurrentLevelData()
        {
            foreach (var levelBuilding in _levelsData.CityData)
            {
                _buildingDatas = levelBuilding.BuildingData;
            }
        }

        private void SetDataToBuildingManagers()
        {
            for (int i = 0; i < buildingManagers.Count; i++)
            {
                buildingManagers[i].BuildingData = _buildingDatas[i];
            }
        }

        private void GetDataFromBuildingManagers()
        {
            for (int i = 0; i < buildingManagers.Count; i++)
            {
                mainPayedAmount.Add(buildingManagers[i].BuildingData.mainBuildingData.PayedAmount);
                mainComplateState.Add(buildingManagers[i].BuildingData.mainBuildingData.CompleteState);
                sidePayedAmount.Add(buildingManagers[i].BuildingData.sideBuildindData.PayedAmount);
                sideComplateState.Add(buildingManagers[i].BuildingData.sideBuildindData.CompleteState);
            }
        }

        public void OnSaveData()
        {
            GetDataFromBuildingManagers();
            SaveSignals.Instance.onSaveIdleParams?.Invoke(SaveIdleParams());
        }
        
        public SaveIdleGameDataParams SaveIdleParams()
        {
            return new SaveIdleGameDataParams()
            {
                IdleLevel = _currentIdleLevel,
                MainBuildingState = mainComplateState,
                MainPayedAmount = mainPayedAmount,
                SideBuildingState = sideComplateState,
                SidePayedAmount = sidePayedAmount
            };
        }
    }
}