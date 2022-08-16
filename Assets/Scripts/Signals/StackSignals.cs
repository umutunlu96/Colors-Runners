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
        public Action<Transform> OnRemoveFromStack;
        public Action<int> OnSetStackStartSize;
        public Action OnLerpStack;
        public Action OnShakeStackSize;
        public Action OnThrowStackInMiniGame;
        public Action<Transform, Transform> onStackOnDronePath;
    }
}
