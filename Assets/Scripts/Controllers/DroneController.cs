using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class DroneController : MonoBehaviour
    {
        [SerializeField] private Transform[] movePath;
        [SerializeField] private float[] moveSpeed;
        [SerializeField] private float delay;

        private Sequence _sequence;

        private void Start()
        {
            SetDroneMovement();
        }

        #region Event Subsicription
    
        void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerEnterDroneArea += OnPlayerEnterDroneArea;
        }
        
        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerEnterDroneArea -= OnPlayerEnterDroneArea;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void SetDroneMovement()
        {
            _sequence = DOTween.Sequence();
            for (int i = 0; i < movePath.Length; i++)
            {
                _sequence.Append(transform.DOMove(movePath[i].position, moveSpeed[i]));
                _sequence.Join(transform.DORotate(
                    new Vector3(movePath[i].localEulerAngles.x, movePath[i].localEulerAngles.y, movePath[i].localEulerAngles.z),
                    moveSpeed[i]));
                _sequence.SetDelay(delay);
            }
            _sequence.Pause();
        }
        
        public void OnPlayerEnterDroneArea()
        {
            _sequence.Play();

            _sequence.OnComplete(() =>
            {
                PlayerSignals.Instance.onPlayerExitDroneArea?.Invoke();
                PlayerSignals.Instance.onDroneAnimationComplated?.Invoke();
                /* Start the color comparison*/
            });//.OnComplete(() => PlayerSignals.Instance.onDroneAnimationComplated?.Invoke());
        }
    }
}