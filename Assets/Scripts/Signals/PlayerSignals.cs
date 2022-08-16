using System;
using Extentions;
using UnityEngine;
using StateMachine;
using Enums;
using UnityEngine.Events;
using UnityEngine.WSA;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public Func<float> onPlayerRotate;
        public Action<Material> onChangeMaterial;
        public Action<AnimationStateMachine> onTranslateAnimationState;
        public Action<OutlineType> onActivateOutlineTrasition;

        protected override void Awake()
        {
            base.Awake();
        }
    }
}