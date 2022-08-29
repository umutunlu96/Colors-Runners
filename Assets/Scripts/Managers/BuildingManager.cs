using System;
using Controllers;
using Data.ValueObject;
using Enums;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Managers
{
    public class BuildingManager : MonoBehaviour
    {
        #region Variables

        #region Public

        public BuildingData BuildingData;
        
        #endregion

        #region Serialized

        [SerializeField] private GameObject mainBuilding;
        [SerializeField] private GameObject sideBuilding;

        [SerializeField] private TextMeshPro mainText;
        [SerializeField] private TextMeshPro sideText;
        
        
        #endregion

        #region Private
        
        private string _mainBuildingName;
        private BuildingComplateState _mainBuildingComplateState;
        private int _mainPrice;
        private int _mainPayedAmount;

        private string _sideBuildingName;
        private BuildingComplateState _sideBuildingComplateState;
        private int _sidePrice;
        private int _sidePayedAmount;
        
        #endregion

        #endregion

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            
        }

        private void UnSubscribeEvents()
        {
            
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        private void Start()
        {
            SetDatas(BuildingData);
            CheckDatas();
        }
        
        public void SetDatas(BuildingData buildingData)
        {
            _mainBuildingName = buildingData.mainBuildingData.BuildingName;
            _mainBuildingComplateState = buildingData.mainBuildingData.CompleteState;
            _mainPrice = buildingData.mainBuildingData.Price;
            _mainPayedAmount = buildingData.mainBuildingData.PayedAmount;

            _sideBuildingName = buildingData.sideBuildindData.BuildingName;
            _sideBuildingComplateState = buildingData.sideBuildindData.CompleteState;
            _sidePrice = buildingData.sideBuildindData.Price;
            _sidePayedAmount = buildingData.sideBuildindData.PayedAmount;
        }

        private void CheckDatas()
        {
            CheckComplateState(_mainBuildingComplateState,_mainPayedAmount,_mainPrice);
            CheckComplateState(_sideBuildingComplateState,_sidePayedAmount,_sidePrice);
        }
        
        public void OnPlayerEnter()
        {
            if (_mainBuildingComplateState == BuildingComplateState.Uncompleted && _sideBuildingComplateState == BuildingComplateState.Uncompleted)
            {
                CheckComplateState(_mainBuildingComplateState,_mainPayedAmount,_mainPrice);
                _mainPayedAmount++;
                SetText(mainText,_mainBuildingName,_mainPayedAmount,_mainPrice);
                SetDataToBuildingData();
            }
            
            else if (_mainBuildingComplateState == BuildingComplateState.Completed && _sideBuildingComplateState == BuildingComplateState.Uncompleted)
            {
                CheckComplateState(_sideBuildingComplateState,_sidePayedAmount,_sidePrice);
                _sidePayedAmount++;
                SetText(sideText,_sideBuildingName,_sidePayedAmount,_sidePrice);
                SetDataToBuildingData();
            }
        }

        public void CheckComplateState(BuildingComplateState complateState, int payedAmount, int price)
        {
            if (complateState == _mainBuildingComplateState && payedAmount >= price)
            {
                _mainBuildingComplateState = BuildingComplateState.Completed;
                mainBuilding.SetActive(false);
                sideBuilding.SetActive(true);
                
                //meshcontroller.alpha = 1 
            }

            if (complateState == _sideBuildingComplateState && payedAmount >= price)
            {
                _sideBuildingComplateState = BuildingComplateState.Completed;
                sideBuilding.SetActive(false);
                //meshcontroller.alpha = 1 
            }
        }

        public void SetText(TextMeshPro text,string BuildingName ,int PayedAmount, int price)
        {
            text.text = BuildingName + "\n" + PayedAmount + " / " + price;
        }

        private void SetDataToBuildingData()
        {
            BuildingData.mainBuildingData.PayedAmount = _mainPayedAmount;
            BuildingData.mainBuildingData.CompleteState = _mainBuildingComplateState;
            
            BuildingData.sideBuildindData.PayedAmount = _sidePayedAmount;
            BuildingData.sideBuildindData.CompleteState = _sideBuildingComplateState;
        }
    }
}