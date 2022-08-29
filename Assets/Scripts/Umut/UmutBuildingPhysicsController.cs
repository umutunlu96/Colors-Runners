// using System;
// using Enums;
// using UnityEngine;
//
// namespace Umut
// {
//     public class UmutBuildingPhysicsController : MonoBehaviour
//     {
//         #region Self Variables
//         [SerializeField] private UmutBuildingManager manager;
//         [SerializeField] private GameObject mainPhysic;
//         [SerializeField] private GameObject sidePhysic;
//         
//         [SerializeField] private BuildingComplateState _mainBuildingComplateState;
//         [SerializeField] private BuildingComplateState _sideBuildingComplateState;
//         
//         #endregion
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
//         }
//         
//         public void CheckData()
//         {
//             switch (_mainBuildingComplateState)
//             {
//                 case BuildingComplateState.Completed:
//                     mainPhysic.gameObject.SetActive(false);
//                     break;
//                 case BuildingComplateState.Uncompleted:
//                     mainPhysic.gameObject.SetActive(true);
//                     break;
//             }
//             
//             switch (_sideBuildingComplateState)
//             {
//                 case BuildingComplateState.Completed:
//                     sidePhysic.gameObject.SetActive(false);
//                     break;
//                 case BuildingComplateState.Uncompleted:
//                     sidePhysic.gameObject.SetActive(false);
//                     break;
//             }
//
//             if (_sideBuildingComplateState == BuildingComplateState.Uncompleted)
//             {
//
//             }
//         }
//     }
// }