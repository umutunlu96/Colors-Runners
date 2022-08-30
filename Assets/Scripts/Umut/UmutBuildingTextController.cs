// using System;
// using Enums;
// using TMPro;
// using UnityEngine;
//
// namespace Umut
// {
//     public class UmutBuildingTextController : MonoBehaviour
//     {
//         #region Self Variables
//         [SerializeField] private UmutBuildingManager manager;
//         [SerializeField] private TextMeshPro mainText;
//         [SerializeField] private TextMeshPro sideText;
//         
//         //SerializeFieldden cikacaklar
//         [SerializeField] private BuildingComplateState _mainBuildingComplateState;
//         [SerializeField] private BuildingComplateState _sideBuildingComplateState;
//         [SerializeField] private string _mainName;
//         [SerializeField] private string _sideName;
//         [SerializeField] private int _mainPayedAmount;
//         [SerializeField] private int _mainPrice;
//         [SerializeField] private int _sidePayedAmount;
//         [SerializeField] private int _sidePrice;
//         
//         #endregion
//         
//         public void SetData(BuildingComplateState mainComplateState, BuildingComplateState sideComplateState
//             ,string mainName, string sideName, int mainPayedAmount, int mainPrice, int sidePayedAmount, int sidePrice)
//         {
//             _mainBuildingComplateState = mainComplateState;
//             _sideBuildingComplateState = sideComplateState;
//             _mainName = mainName;
//             _mainPayedAmount = mainPayedAmount;
//             _mainPrice = mainPrice;
//             _sideName = sideName;
//             _sidePayedAmount = sidePayedAmount;
//             _sidePrice = sidePrice;
//         }
//
//         public void SetData(BuildingComplateState mainComplateState, BuildingComplateState sideComplateState)
//         {
//             _mainBuildingComplateState = mainComplateState;
//             _sideBuildingComplateState = sideComplateState;
//         }
//         
//         private void Start()
//         {
//             CheckData();
//             mainText.text = _mainName + "\n" + _mainPayedAmount + " / " + _mainPrice;
//         }
//
//         public void CheckData()
//         {
//             switch (_mainBuildingComplateState)
//             {
//                 case BuildingComplateState.Completed:
//                     mainText.gameObject.SetActive(false);
//                     break;
//                 case BuildingComplateState.Uncompleted:
//                     mainText.gameObject.SetActive(true);
//                     break;
//             }
//             
//             switch (_sideBuildingComplateState)
//             {
//                 case BuildingComplateState.Completed:
//                     sideText.gameObject.SetActive(false);
//                     break;
//                 case BuildingComplateState.Uncompleted:
//                     sideText.gameObject.SetActive(true);
//                     break;
//             }
//
//             if (_sideBuildingComplateState == BuildingComplateState.Uncompleted)
//             {
//
//             }
//         }
//
//         private void CheckPayAmount(int payedAmount, int price, string buildingName)
//         {
//             if (payedAmount >= price)
//             {
//                 print(buildingName +" Maximum Reached");
//                 // manager.CheckAreaState(buildingName);
//             }
//         }
//
//         public void UpdatePayedAmount(string buildingName)
//         {
//             if (buildingName.Equals("Main") && _mainBuildingComplateState == BuildingComplateState.Uncompleted)
//             {
//                 _mainPayedAmount++;
//                 mainText.text = _mainName + "\n" + _mainPayedAmount + " / " + _mainPrice;
//                 CheckPayAmount(_mainPayedAmount, _mainPrice, buildingName);
//             }
//             
//             else if (buildingName.Equals("Side") && _sideBuildingComplateState == BuildingComplateState.Uncompleted)
//             {
//                 _sidePayedAmount++;
//                 sideText.text = _sideName + "\n" + _sidePayedAmount + " / " + _sidePrice;
//                 CheckPayAmount(_sidePayedAmount, _sidePrice, buildingName);
//             }
//         }
//         
//         
//     }
// }