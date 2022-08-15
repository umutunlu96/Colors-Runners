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
            if(other.CompareTag("Collectable") && _manager.transform.CompareTag("Collected"))
            {
               // _manager.AddCollectableToStackManager();
            }

            //test purposes
            if(other.CompareTag("Player") && !_manager.transform.CompareTag("Collected") )
            {
                _manager.AddCollectableToStackManager();
            }

            if(other.CompareTag("Obstical"))
            {
                _manager.RemoveCollectableFromStackManager();
                Destroy(other.gameObject); 
            }

            if (other.CompareTag("MatObstical"))
            {
                StackSignals.Instance.onStackOnDronePath(transform.parent, other.transform);
                Debug.Log("is working");
            }
        }
    }
}