using System;
using System.Collections;
using Managers;
using Signals;
using StateMachine;
using UnityEngine;

namespace Controllers
{
    public class CollectableAnimationController : MonoBehaviour
    {

        #region SelfVariables

        #region Serialize Variables

        [SerializeField] private CollectableManager manager;
        [SerializeField] private ParticleSystem particleSystem; //Particle eklenecek

        #endregion

        #region Private Variavles

        private Animator _collectableAnimator;
        private AnimationStateMachine _collectableStateMachine;

        #endregion
        #endregion
        
        private void Awake()
        {
            _collectableAnimator = GetComponent<Animator>();
            _collectableStateMachine = new IdleAnimationState();
            _collectableStateMachine.SetContext(ref _collectableAnimator);
            _collectableStateMachine.ChangeAnimationState();
            
            if (manager.GetComponentInChildren<CollectablePhysicController>().CompareTag("Collected"))
            {
                TranslateAnimationState(new SneakIdleAnimationState());
            }
        }
        
        #region Subscriptions

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnSubscribe()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
        #endregion

        public void TranslateAnimationState(AnimationStateMachine state)
        {
            _collectableStateMachine = state;
            _collectableStateMachine.SetContext(ref _collectableAnimator);
            _collectableStateMachine.ChangeAnimationState();
        }

        private void OnPlay()
        {
            if (manager.GetComponentInChildren<CollectablePhysicController>().CompareTag("Collected"))
            {
                TranslateAnimationState(new RunnerAnimationState());
            }
        }
        
        private void ActivateParticule()
        {

        }

        private void DestroyManager()
        {
            // manager.gameObject.SetActive(false);
        }
    }
}