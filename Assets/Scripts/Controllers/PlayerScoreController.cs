using System;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class PlayerScoreController : MonoBehaviour
    {
        [SerializeField] private TextMeshPro scoreText;

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvents();
        }


        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void SubscribeEvents()
        {
            
        }

        private void UnSubscribeEvents()
        {
            
        }

        #endregion
        
        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}