using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class CollectablePhysicController : MonoBehaviour
    {

        #region Self Variables

        #region Serialize Variables

        [SerializeField] private CollectableManager manager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable") && CompareTag("Collected"))
            {
                other.tag = "Collected";
                if (manager.CompareColor(other.transform.parent.GetComponent<CollectableManager>().CurrentColorType))
                {
                    manager.AddCollectableToStackManager(other.transform.parent); //sinyali burdan gonder.
                }
                else if (!manager.CompareColor(other.transform.parent.GetComponent<CollectableManager>()
                    .CurrentColorType)) //call from signals
                {
                    manager.RemoveCollectableFromStackManager(transform.parent);
                    other.transform.parent.gameObject.SetActive(false);
                }
                return;
            }

            if (other.CompareTag("Obstacle"))
            {
                manager.RemoveCollectableFromStackManager(manager.transform);
                other.gameObject.SetActive(false);
                return;
            }
            
            if (other.CompareTag("MatTrigger"))
            {
                StackSignals.Instance.onStackEnterDroneArea?.Invoke(manager.transform, other.transform);
                
                if (!manager.CompareColor(other.GetComponent<MatController>().currentColorType))
                {
                    manager.IsDead = true;
                }
                return;
            }

            if (other.CompareTag("TurretMatTrigger"))
            {
                if (!manager.CompareColor(other.GetComponent<TurretMatController>().currentColorType))
                {
                    StackSignals.Instance.onWrongTurretMatAreaEntered?.Invoke(manager.transform);
                }
                return;
            }
        }
    }
}