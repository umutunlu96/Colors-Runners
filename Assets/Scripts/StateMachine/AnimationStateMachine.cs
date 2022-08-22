using UnityEngine;

namespace StateMachine
{
    public abstract class AnimationStateMachine
    {
        protected Animator ? _animator { get; set; }
        
        public void SetContext(ref Animator animator)
        {
            _animator = animator;
        }

        public abstract void ChangeAnimationState();
    }
}