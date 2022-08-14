using System;
using Controllers;
using Signals;
using StateMachine;
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

        #endregion

        #endregion

        #region Subscriptions

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            PlayerSignals.Instance.onChangeMaterial += OnSetCollectableMaterial;
            PlayerSignals.Instance.onTranslateAnimationState += OnTranslateAnimationState;
        }

        private void UnSubscribe()
        {

            PlayerSignals.Instance.onChangeMaterial -= OnSetCollectableMaterial;
            PlayerSignals.Instance.onTranslateAnimationState -= OnTranslateAnimationState;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
        #endregion

        private void OnTranslateAnimationState(AnimationStateMachine state)
        {
            animatorController.TranslateAnimationState(state);
        }

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
            animatorController.transform.rotation = new Quaternion(0, 0, 0,0);
        }

        public void RemoveCollectableFromStackManager()
        {

        }

        public void RotateMeshForward()
        {
            StackSignals.Instance.OnRemoveFromStack(transform);
        }
    }
}
