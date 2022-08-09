using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Action<GameObject> onAddStack;
        public Action<int> OnRemoveFromStack;
        public Action<int> OnSetStackStartSize;
        public Action OnLerpStack;
        public Action OnShakeStackSize;
        public Action OnThrowStackInMiniGame;
    }
}
