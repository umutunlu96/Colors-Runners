using Managers;
using Signals;
using UnityEngine;
using MK;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        [SerializeField] PlayerManager manager;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Ramp"))
            {
                manager.JumpPlayerOnRamp();
                other.gameObject.GetComponent<Collider>().enabled = false;
            }

            if(other.CompareTag("Gate"))
            {
<<<<<<< HEAD
                Color color = other.gameObject.GetComponent<MeshRenderer>().material.color;
                PlayerSignals.Instance.onChangeColor?.Invoke(color);
=======
                Material color = other.GetComponent<MeshRenderer>().material;
                PlayerSignals.Instance.onChangeMaterial(color);
>>>>>>> origin/CollectableManager
            }
        }
    }
}