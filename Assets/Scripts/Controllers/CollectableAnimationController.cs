using System.Collections;
using Managers;
using StateMachine;
using UnityEngine;

namespace Controllers
{
    public class CollectableAnimationController : MonoBehaviour
    {

        #region SelfVariables

        #region Serialize Variables

        [SerializeField] CollectableManager manager;

        #endregion

        #region Private Variavles

        private ParticleSystem _particleSystem;
        private Animator _CollectableAnimator;
        private AnimationStateMachine _CollectableStateMachine;

        #endregion
        #endregion

        private void Awake()
        {
            _CollectableAnimator = GetComponent<Animator>();
            _CollectableStateMachine = GetComponent<RunnerAnimationState>();
            _CollectableStateMachine.SetContext(ref _CollectableAnimator);
            _CollectableStateMachine.ChangeAnimationState();
        }

        public void TranslateAnimationState(AnimationStateMachine state)
        {
            _CollectableStateMachine = state;
            _CollectableStateMachine.SetContext(ref _CollectableAnimator);
            _CollectableStateMachine.ChangeAnimationState();
        }

      
        private void ActivateParticul()
        {

        }
    }
}