using System;
using System.Collections;
using DG.Tweening;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class TurretController : MonoBehaviour
    {
        [SerializeField] private GameObject denemeeeeee;
        [SerializeField] private Vector2 turretMaxAngleX;
        [SerializeField] private Vector2 turretMaxAngleY;
        [SerializeField] private GameObject turret;
        [SerializeField] private ParticleSystem fireParticle;
        [SerializeField] private float turnSpeed;
        [SerializeField] private float turnDelay;
        private bool _isAiming, _isRotating;
        private void Start()
        {
            Idle();
        }
        
        private void Idle()
        {
            if (!_isAiming && !_isRotating)
            {
                _isRotating = true;
                float randomRotateX = Random.Range(turretMaxAngleX.x, turretMaxAngleX.y);
                float randomRotateY = Random.Range(turretMaxAngleY.x, turretMaxAngleY.y);
                Vector3 randomRotationVector = new Vector3(randomRotateX, randomRotateY - 180f, 0);
                turret.transform.DORotate(randomRotationVector, turnSpeed).SetDelay(turnDelay * 2).OnComplete(() => {
                    _isRotating = false; Idle();});
            }
        }
        
        private void Aim(Transform target)
        {
            _isAiming = true;
            turret.transform.DOLookAt(target.position, turnSpeed).SetDelay(turnDelay / 4).OnComplete(() =>
            {
                fireParticle.Play();
                StackSignals.Instance.OnRemoveFromStack?.Invoke(target);
            });
        }

        public void TriggetAim()
        {
            Aim(denemeeeeee.transform);
        }
    }
}