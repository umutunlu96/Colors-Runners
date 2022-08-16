using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
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
            DoAnim();
        }

        public void DoAnim()
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
        }
    }
}