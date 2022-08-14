using System.Collections;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region SelfVariables

        #region Private Variavles

        private SkinnedMeshRenderer _renderer;

        #endregion

        #region Serialize Variavles

        [SerializeField] CollectableManager _manager;

        #endregion

        #endregion

        private void Awake()
        {
<<<<<<< HEAD
            _renderer = GetComponent<SkinnedMeshRenderer>();
=======
            _material = GetComponent<SkinnedMeshRenderer>().material;
>>>>>>> origin/CollectableManager
        }

        private void SetParticulColor(Color color)
        {

        }

        public void SetCollectableMatarial(Material material)
        {
<<<<<<< HEAD
            _renderer.material = material;
=======
            _material.color = material.color;
>>>>>>> origin/CollectableManager
        }

        private void AcrivateOutlineTrasition(/*state*/)
        {

        }
    }
}