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
            if(other.CompareTag("Collectable") && CompareTag("Collected"))
            {
                _manager.AddCollectableToStackManager();
            }

            //test purposes
            if(other.CompareTag("Player") && CompareTag("Collectable"))
            {
<<<<<<< HEAD
                StackSignals.Instance.onAddStack?.Invoke(transform.parent);
                _manager.RotateMeshForward();
=======
                _manager.AddCollectableToStackManager();
>>>>>>> origin/CollectableManager
            }

            if(other.CompareTag("Obstical"))
            {
                _manager.RemoveCollectableFromStackManager();
                Destroy(other.gameObject); 
            }
        }
    }
}