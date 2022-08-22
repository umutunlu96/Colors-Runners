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
        
        #region Public Variable

        public ColorType CurrentColorType;

        #endregion
        
        #region Serialize Variables

        [SerializeField] CollectableMeshController meshController;
        [SerializeField] CollectablePhysicController physicController;
        [SerializeField] CollectableAnimationController animatorController;


        #endregion

        #region Private Variables
        
        private bool _isDead;
        
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
            StackSignals.Instance.onActivateOutlineTrasition += OnActivateOutlineTransition;
            StackSignals.Instance.onDroneKillsCollectables += OnDroneKillsCollectables;
            StackSignals.Instance.onDroneAnimationComplated += OnDroneAnimationCompleted;
        }

        private void UnSubscribe()
        {
            PlayerSignals.Instance.onChangeMaterial -= OnSetCollectableMaterial;
            PlayerSignals.Instance.onTranslateAnimationState -= OnTranslateAnimationState;
            StackSignals.Instance.onActivateOutlineTrasition -= OnActivateOutlineTransition;
            StackSignals.Instance.onDroneKillsCollectables -= OnDroneKillsCollectables;
            StackSignals.Instance.onDroneAnimationComplated -= OnDroneAnimationCompleted;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
        #endregion

        public void ChangeMaterialColor(ColorType type)
        {
            CurrentColorType = type;
            meshController.ChangeMaterialColor();
        }

        private void OnTranslateAnimationState(AnimationStateMachine state)
        {
            if(physicController.CompareTag("Collected"))
            {
                animatorController.TranslateAnimationState(state);
            }
        }

        private void OnSetCollectableMaterial(Material material)
        {
            if(physicController.CompareTag("Collected"))
            {
                meshController.SetCollectableMaterial(material);
            }
        }

        public bool CompareColor(ColorType type)
        {
            return CurrentColorType == type;
        }

        private void OnActivateOutlineTransition(OutlineType type)
        {
            if(physicController.CompareTag("Collected"))
            {
                meshController.ActivateOutlineTransition(type);
            }
        }

        public void AddCollectableToStackManager(Transform collectableTransform)
        {
            StackSignals.Instance.onAddStack?.Invoke(collectableTransform);
            collectableTransform.rotation = new Quaternion(0, 0, 0,0);
        }

        public void RemoveCollectableFromStackManager(Transform collectableTransform)
        {
            StackSignals.Instance.onRemoveFromStack?.Invoke(collectableTransform);
        }

        public void RotateMeshForward()
        {
            StackSignals.Instance.onRemoveFromStack(transform);
        }

        private void OnDroneKillsCollectables()
        {
            if(_isDead)
            {
                OnTranslateAnimationState(new DeathAnimationState());
            }
            else
            {
                OnTranslateAnimationState((new RunnerAnimationState()));
            }
        }
        
        private void OnDroneAnimationCompleted()
        {
            if(!_isDead)
            {
                meshController.ActivateOutlineTransition(OutlineType.Outline);
            }

            StackSignals.Instance.onAddAfterDroneAnimationDone?.Invoke(_isDead, transform);
        }

        public bool IsDead {get =>_isDead;  set => _isDead = value;}
    }
}