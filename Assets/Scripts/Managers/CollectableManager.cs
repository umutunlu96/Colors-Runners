using System;
using Controllers;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {

        #region SelfVariables

        #region Serialize Variables

        [SerializeField] CollectableMeshController meshController;
        [SerializeField] CollectablePhisicController phisicController;
        [SerializeField] CollectableAnimationController animatorController;

        #endregion

        #region Private Variables
<<<<<<< HEAD
        //write collectable State
        private Material _playerMat;

=======
      
>>>>>>> origin/CollectableManager
        #endregion

        #endregion

        private void Awake()
        {
            _playerMat = GameObject.FindObjectOfType<PlayerMeshController>().GetComponent<SkinnedMeshRenderer>()
                .material;
        }

        #region Subscriptions

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            PlayerSignals.Instance.onChangeMaterial += OnSetCollectableMaterial;
        }

        private void UnSubscribe()
        {

            PlayerSignals.Instance.onChangeMaterial -= OnSetCollectableMaterial;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
        #endregion

        private void OnSetCollectableMaterial(Material material)
        {
            if(CompareTag("Collected"))
            {
                meshController.SetCollectableMatarial(material);
            }
        }

        public void AddCollectableToStackManager()
        {
            StackSignals.Instance.onAddStack(transform);
        }

        public void RemoveCollectableFromStackManager()
        {
<<<<<<< HEAD
            meshController.SetMatarial(_playerMat);
        }

        public void RotateMeshForward()
        {
            animatorController.transform.rotation = new Quaternion(0, 0, 0,0);
=======
            StackSignals.Instance.OnRemoveFromStack(transform);
>>>>>>> origin/CollectableManager
        }
    }
}
