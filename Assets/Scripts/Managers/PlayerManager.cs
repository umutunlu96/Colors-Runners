using System;
using Controllers;
using Data.ValueObject;
using UnityEngine;

namespace Managers
{
    public class PlayerManager: MonoBehaviour
    {
        private PlayerData playerData;
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        [SerializeField] private PlayerMeshController playerMeshController;
        [SerializeField] private PlayerAnimationController playerAnimationController;

        private void GetPlayerData()
        {
            
        }

        private void SetPlayerDataToControllers()
        {
            
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