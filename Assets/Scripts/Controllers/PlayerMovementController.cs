using System;
using Data.ValueObject;
using Enums;
using Keys;
using Unity.Mathematics;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidBody;

        private PlayerMovementData _playerMovementData;
        private float _horizontalInput;
        private float _verticalInput;
        private bool _isReadyToMove;
        private bool _runnerMovement;
        private bool _idleMovement;
        
        
        public void SetMovementData(PlayerMovementData movementData)
        {
            _playerMovementData = movementData;
        }

        private void FixedUpdate()
        {
            if (_isReadyToMove)
            {
                if (_runnerMovement)
                {
                    RunnerMove();
                    RunnerRotate();
                }
                else if (_idleMovement)
                {
                    IdleMove();
                    IdleRotate();
                }
                else
                    Stop();
            }
        }

        public void ActivateMovement()
        {
            _isReadyToMove = true;
        }

        public void DeactivateMovement()
        {
            _isReadyToMove = false;
            Stop();
        }
        
        private void RunnerMove()
        {
            rigidBody.velocity = new Vector3(_horizontalInput * _playerMovementData.RunnerSidewaySpeed, rigidBody.velocity.y,
                _playerMovementData.RunnerForwardSpeed);
        }

        private void RunnerRotate()
        {
            Vector3 direction = Vector3.forward + Vector3.right * Mathf.Clamp(_horizontalInput,
                -_playerMovementData.RunnerMaxRotateAngle, _playerMovementData.RunnerMaxRotateAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                _playerMovementData.RunnerTurnSpeed);
        }
        
        private void IdleMove()
        {
            rigidBody.velocity = new Vector3(_horizontalInput * _playerMovementData.IdleSpeed, rigidBody.velocity.y,
                 _verticalInput * _playerMovementData.IdleSpeed);
        }

        private void IdleRotate()
        {
            if (_verticalInput != 0 || _horizontalInput != 0)
            {
                Vector3 direction = Vector3.forward * _verticalInput + Vector3.right * _horizontalInput;

                #region RigidbodyRotation
                // rigidBody.rotation = Quaternion.Slerp(rigidBody.rotation, Quaternion.LookRotation(direction),
                //     _playerMovementData.IdleTurnSpeed);
                #endregion

                #region TransformFastRotation
                // Quaternion lookDirection = Quaternion.LookRotation(direction);
                // transform.rotation = lookDirection;
                #endregion
                
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                    _playerMovementData.IdleTurnSpeed);
            }
        }

        private void Stop()
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }
        
        public void SetInputValues(InputParams inputParams)
        {
            _horizontalInput = inputParams.XValue;
            _verticalInput = inputParams.YValue;
        }

        public void ChangeMovementType(JoystickStates joystickState)
        {
            switch (joystickState)
            {
                case JoystickStates.Runner:
                    _runnerMovement = true;
                    _idleMovement = false;
                    break;
                case JoystickStates.Idle:
                    _runnerMovement = false;
                    _idleMovement = true;
                    break;
            }
        }
        
        public void SetMovementValues(float horizontalInput, float verticalInput)
        {
            _horizontalInput = horizontalInput;
            _verticalInput = verticalInput;
        }
    }
}