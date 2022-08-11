using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {

        #region Self Variables

        #region Private Variables

        private CinemachineVirtualCamera _RunnerCam;
        private CinemachineVirtualCamera _IdleCam;
        private Animator _animator;

        #endregion

        #endregion

        private void Awake()
        {
            _RunnerCam = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
            _RunnerCam = transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
            _animator = GetComponent<Animator>();
        }
    }
}