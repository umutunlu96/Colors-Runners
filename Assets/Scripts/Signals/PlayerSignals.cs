using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public Func<float> onPlayerRotate;
        //change collectable material signals
        public Action<Material> onChangeMaterial;

        protected override void Awake()
        {
            base.Awake();
        }
    }
}