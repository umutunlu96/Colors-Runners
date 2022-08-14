using System;
using Extentions;
using UnityEngine;
using StateMachine;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public Func<float> onPlayerRotate;
        public Action<Material> onChangeMaterial;
        public Action<AnimationStateMachine> onTranslateAnimationState;

        protected override void Awake()
        {
            base.Awake();
        }
    }
}