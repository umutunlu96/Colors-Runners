using System;
using UnityEngine;
using Extentions;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public Action<Transform> onAddStack;
        public Action<Transform> onRemoveFromStack;
        public Action<int> onSetStackStartSize;
        public Action onLerpStack;
        public Action onShakeStackSize;
        public Action onThrowStackInMiniGame;
        public Action<Transform, Transform> onStackOnDronePath;
        public Action onMergeToPLayer;
        public Action<bool, Transform> onAddAfterDroneAnimationDone;
    }
}
