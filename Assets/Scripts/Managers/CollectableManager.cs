using System;
using Controllers;
using Enums;
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

        [SerializeField] private bool _isDead;
        #endregion

        #region public Variable

        public ColorType currentColorType;

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
            PlayerSignals.Instance.onActivateOutlineTrasition += OnActivateOutlineTrasition;
            PlayerSignals.Instance.onDroneAnimationComplated += OnDroneAnimationComplated;
        }

        private void UnSubscribe()
        {
            PlayerSignals.Instance.onChangeMaterial -= OnSetCollectableMaterial;
            PlayerSignals.Instance.onTranslateAnimationState -= OnTranslateAnimationState;
            PlayerSignals.Instance.onActivateOutlineTrasition -= OnActivateOutlineTrasition;
            PlayerSignals.Instance.onDroneAnimationComplated -= OnDroneAnimationComplated;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
        #endregion

        public void ChangeMatarialColor(ColorType type)
        {
            currentColorType = type;
            meshController.ChangeMatarialColor();
        }

        private void OnTranslateAnimationState(AnimationStateMachine state)
        {
            if(CompareTag("Collected"))
            {
                animatorController.TranslateAnimationState(state);
            }
        }

        private void OnSetCollectableMaterial(Material material)
        {
            if(CompareTag("Collected"))
            {
                meshController.SetCollectableMatarial(material);
            }
        }

        public bool CompareColor(ColorType type)
        {
            if (currentColorType == type)
            {
                return true;
            }
            else return false;
        }

        private void OnActivateOutlineTrasition(OutlineType type)
        {
            if(CompareTag("Collected"))
            {
                meshController.ActivateOutlineTrasition(type);
            }
        }

        public void AddCollectableToStackManager(Transform _transform)
        {

            StackSignals.Instance.onAddStack(_transform);
            _transform.rotation = new Quaternion(0, 0, 0,0);
        }

        public void RemoveCollectableFromStackManager(Transform transform)
        {
            StackSignals.Instance.onRemoveFromStack?.Invoke(transform);
        }


        public void RotateMeshForward()
        {
            StackSignals.Instance.onRemoveFromStack(transform);
        }

        private void OnDroneAnimationComplated()
        {
            if(_isDead == true)
            {
                OnTranslateAnimationState(new DeathAnimationState());
            }
            StackSignals.Instance.onAddAfterDroneAnimationDone?.Invoke(_isDead, transform);
        }

        public bool IsDead { get { return _isDead; } set { _isDead = value; } }
    }
}
