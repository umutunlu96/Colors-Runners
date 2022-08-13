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
        //write collectable State

        #endregion

        #endregion

        
        #region Subscriptions

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            PlayerSignals.Instance.onChangeMaterial += OnSetMaterial;
        }

        private void UnSubscribe()
        {

            PlayerSignals.Instance.onChangeMaterial -= OnSetMaterial;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
        #endregion

        private void OnSetMaterial(Material material)
        {
            meshController.SetMatarial(material);
        }
    }
}
