using System.Collections;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region SelfVariables

        #region Private Variavles

        private Material _material;

        #endregion

        #region Serialize Variavles

        [SerializeField] CollectableManager _manager;

        #endregion

        #endregion

        private void Awake()
        {
            _material = GetComponent<SkinnedMeshRenderer>().material;
        }

        private void SetParticulColor(Color color)
        {

        }

        public void SetCollectableMatarial(Material material)
        {
            _material.color = material.color;
        }

        private void AcrivateOutlineTrasition(/*state*/)
        {

        }
    }
}