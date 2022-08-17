using System;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class CollectablePhisicController : MonoBehaviour
    {

        #region Self Variables

        #region Serialize Variables

        [SerializeField] CollectableManager _manager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {

            try
            {
                if (other.CompareTag("Collectable") && CompareTag("Collected"))
                {
                    if (_manager.CompareColor(other.transform.parent.GetComponent<CollectableManager>().currentColorType))
                    {
                        _manager.AddCollectableToStackManager(other.transform.parent);
                    }
                    else if (!_manager.CompareColor(other.transform.parent.GetComponent<CollectableManager>().currentColorType))//call from signals
                    {
                        _manager.RemoveCollectableFromStackManager(transform.parent);
                        other.transform.parent.gameObject.SetActive(false);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("error sourch : " + e.Source);
                Debug.Log("error inner sourch : " + e.InnerException);

                Debug.Log("error message : " + e.Message);

                Debug.Log("error data : " + e.Data);
                Debug.Log("error data : " + e.Data.ToString());
            }
          
            if (other.CompareTag("Obstical"))
            {
                _manager.RemoveCollectableFromStackManager(_manager.transform);
                other.gameObject.SetActive(false);
            }

            if (other.CompareTag("MatObstical"))
            {
                StackSignals.Instance.onStackOnDronePath(_manager.transform, other.transform);
                if (!_manager.CompareColor(other.GetComponent<MatController>().currentColorType))
                {
                    _manager.IsDead = true;
                }

            }

            
            
        }
    }
}