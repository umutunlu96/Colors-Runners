using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerManager: MonoBehaviour
    {
        private PlayerData _playerData;
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        [SerializeField] private PlayerMeshController playerMeshController;
        [SerializeField] private PlayerAnimationController playerAnimationController;

        #region Event Subsicription
    
        void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            InputSignals.Instance.onPointerDown += OnPointerDown;
            InputSignals.Instance.onPointerDragged += OnInputDragged;
            InputSignals.Instance.onPointerReleased += OnInputReleased;
            InputSignals.Instance.onInputParamsUpdate += OnInputParamsUpdate;
            InputSignals.Instance.onJoystickStateChange += OnJoystickStateChange;
        }
        
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            InputSignals.Instance.onPointerDown -= OnPointerDown;
            InputSignals.Instance.onPointerDragged -= OnInputDragged;
            InputSignals.Instance.onPointerReleased -= OnInputReleased;
            InputSignals.Instance.onInputParamsUpdate -= OnInputParamsUpdate;
            InputSignals.Instance.onJoystickStateChange -= OnJoystickStateChange;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void Awake()
        {
            _playerData = GetPlayerData();
            SetPlayerDataToControllers();
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;
        
        private void SetPlayerDataToControllers()
        {
            playerMovementController.SetMovementData(_playerData.playerMovementData);
        }

        private void OnPlay()
        {
            playerMovementController.ActivateMovement();
            playerAnimationController.SetAnimationState(SticmanAnimationType.Run);
        }

        private void OnPointerDown()
        {
            playerAnimationController.SetAnimationState(SticmanAnimationType.Run);
        }
        
        private void OnInputDragged()
        {
            // playerMovementController.ActivateMovement();
        }
        
        private void OnInputReleased()
        {
            playerAnimationController.SetAnimationState(SticmanAnimationType.Idle);
            playerMovementController.SetInputValues(new InputParams() { XValue = 0, YValue = 0,});
        }

        private void OnInputParamsUpdate(InputParams inputParams)
        {
            playerMovementController.SetInputValues(inputParams);
        }      
        
        private void OnJoystickStateChange(JoystickStates joystickState)
        {
            playerMovementController.ChangeMovementType(joystickState);
            switch (joystickState)
            {
                case JoystickStates.Runner:
                    playerAnimationController.SetAnimationState(SticmanAnimationType.Run);
                    break;
                case JoystickStates.Idle:
                    playerAnimationController.SetAnimationState(SticmanAnimationType.Idle);
                    break;
            }
        }
        
        private void OnChangePlayerGradientColor()
        {
            
        }

        private void OnChanePlayerColor()
        {
            
        }

        private void ActivateMovement()
        {
            
        }

        private void DeactivateMovement()
        {
            
        }

        private void OnReset()
        {
            
        }
        
    }
}