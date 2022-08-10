using System.Collections;
using System.Collections.Generic;
using Enums;
using Signals;
using UnityEngine;

namespace Umut
{
    public class DenemeUmut : MonoBehaviour
    {
        public void Runner()
        {
            InputSignals.Instance.onJoystickStateChange?.Invoke(JoystickStates.Runner);
            print("x");
        }

        public void Idle()
        {
            InputSignals.Instance.onJoystickStateChange?.Invoke(JoystickStates.Idle);
        }

        public void OnPlay()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }
    }
}
