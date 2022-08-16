﻿using Managers;
using Signals;
using DG.Tweening;
using UnityEngine;


namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        [SerializeField] PlayerManager manager;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Ramp"))
            {
                manager.JumpPlayerOnRamp();
                other.gameObject.GetComponent<Collider>().enabled = false;
            }

            if(other.CompareTag("Gate"))
            {
                Material color = other.GetComponent<MeshRenderer>().material;
                PlayerSignals.Instance.onChangeMaterial(color);
            }
            if(other.CompareTag("MatObstical"))
            {
                //manager.DeactivateMovement();
                manager.transform.DOMoveZ(10, 3f).SetRelative();
            }
        }
    }
}