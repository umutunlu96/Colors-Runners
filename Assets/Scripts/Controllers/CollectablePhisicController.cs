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
                _manager.AddCollectableToStackManager();
            }

            if(other.CompareTag("Obstical"))
            {
                _manager.RemoveCollectableFromStackManager();
                Destroy(other.gameObject); 
            }
        }
    }
}