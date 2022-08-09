using System.Collections;
using Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers
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
            //write trigers
        }
    }
}