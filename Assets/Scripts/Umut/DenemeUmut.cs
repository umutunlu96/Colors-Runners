using System.Collections;
using System.Collections.Generic;
using Enums;
using StateMachine;
using Signals;
using UnityEngine;

namespace Umut
{
    public class DenemeUmut : MonoBehaviour
    {
        public void Asd()
        {
            SaveSignals.Instance.onIdleSaveData?.Invoke();
        }
    }
}
