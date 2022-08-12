using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class CollectablePhisicController : MonoBehaviour
    {

        #region Self Variables

        #region Serialize Variables

        //[SerializeField] CollectableManager _manager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Collectable") && CompareTag("Collected"))
            {
                StackSignals.Instance.onAddStack(other.transform);
                Debug.Log("trigger on collectable");
            }

            if(other.CompareTag("Player"))
            {
                StackSignals.Instance.onAddStack(transform);
                Debug.Log("trigger on player");
            }
        }
    }
}