using Managers;
using StateMachine;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region SelfVariables

        
        #region Serialize Variables

        [SerializeField] private PlayerManager manager;

        #endregion Serialize Variables

        #region Private Variavles

        private ParticleSystem _particleSystem;
        private Animator _playereAnimator;
        private AnimationStateMachine _playerStateMachine;

        #endregion Private Variavles

        #endregion SelfVariables

        private void Awake()
        {
            Initialize();
        }

        public void TranslatePlayerAnimationState(AnimationStateMachine state)
        {
            _playerStateMachine = state;
            _playerStateMachine.SetContext(ref _playereAnimator);
            _playerStateMachine.ChangeAnimationState();
        }

        private void Initialize()
        {
            _playereAnimator = GetComponent<Animator>();
            _playerStateMachine = GetComponent<AnimationStateMachine>();
            TranslatePlayerAnimationState(new IdleAnimationState());
        }
    }
}