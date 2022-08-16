using System;
using Extentions;
using UnityEngine;
using StateMachine;
using UnityEngine.Events;
using UnityEngine.WSA;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public Func<float> onPlayerRotate;
        public Action onPlayerEnterDroneArea;
        public Action onPlayerExitDroneArea;
        public Action onPlayerEnterTurretArea;
        public Action onPlayerExitTurretArea;
        public Action<Material> onChangeMaterial;
        public Action<AnimationStateMachine> onTranslateAnimationState;

        protected override void Awake()
        {
            base.Awake();
        }
    }
}