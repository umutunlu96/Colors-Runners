using Controllers;
using Enums;
using Signals;
using UnityEngine;
using System.Threading.Tasks;

namespace Managers
{
    public class DroneAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion
        
        #region SeriliazeField
        
        [SerializeField] private MatController matControllerLeft;
        [SerializeField] private ColorType leftMatColorType;
        [SerializeField] private MatController matControllerRight;
        [SerializeField] private ColorType rightMatColorType;
        [SerializeField] private DroneController droneController;
        [SerializeField] private bool matLeft, matRight;
        #endregion

        #region Private Variables
        
        #endregion
        #endregion

        private void Awake()
        {
            SendColorTypeToMats();
        }
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onLastCollectableEnterDroneArea += OnLastCollectableEnterDroneArea;
        }

        private void UnSubscribeEvents()
        {
            StackSignals.Instance.onLastCollectableEnterDroneArea += OnLastCollectableEnterDroneArea;
        }
        
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        #endregion
        
        private void Start()
        {
            SetColorOfMats();
        }

        #region SetMatsColors

        private void SendColorTypeToMats()
        {
            matControllerLeft.GetColorData(leftMatColorType);
            matControllerRight.GetColorData(rightMatColorType);
        }
        
        private void SetColorOfMats()
        {
            matControllerLeft.SetColorData(leftMatColorType);
            matControllerRight.SetColorData(rightMatColorType);
        }

        #endregion


        private void DisableMatControllersCollider()
        {
            matControllerLeft.DisableBoxCollider();
            matControllerRight.DisableBoxCollider();
        }

        private void CloseUpMat()
        {
            if(!matLeft)
                matControllerLeft.CloseMat();
            else if(!matRight)
                matControllerRight.CloseMat();
        }
        
        private async void OnLastCollectableEnterDroneArea()
        {
            print("LastCollectable");
            await Task.Delay(1000);
            print("Outline changed");
            StackSignals.Instance.onActivateOutlineTrasition?.Invoke(OutlineType.NonOutline);
            await Task.Delay(1000);
            CloseUpMat();
            print("MatsAreClosing");
            await Task.Delay(250);
            droneController.StartDroneAnimation();
            await Task.Delay(3000);
            StackSignals.Instance.onDroneKillsCollectables?.Invoke();
            print("Drone started");
            DisableMatControllersCollider();
        }
    }
}