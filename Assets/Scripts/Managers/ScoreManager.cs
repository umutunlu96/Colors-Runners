using System;
using Keys;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Variables

        private int _currentScore;
        private int _totalScore;
        private Transform _target;
        private TextMeshPro _scoreText;

        [SerializeField] private Vector3 followOffset;
        #endregion

        private void Awake()
        {
            _scoreText = GetComponent<TextMeshPro>();
            _scoreText.text = "";
        }

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
            ScoreSignals.Instance.onCurrentLevelScoreUpdate += OnCurrentLevelScoreUpdate;
            ScoreSignals.Instance.onTotalScoreUpdate += OnTotalScoreUpdate;
            
            ScoreSignals.Instance.currentScore += ReturnCurrentScore;
            ScoreSignals.Instance.totalScore += ReturnTotalScore;

            StackSignals.Instance.onSetScoreControllerPosition += OnSetPosition;

            CoreGameSignals.Instance.onPlay += OnFindFollowTarget;
        }
        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.onCurrentLevelScoreUpdate -= OnCurrentLevelScoreUpdate;
            ScoreSignals.Instance.onTotalScoreUpdate -= OnTotalScoreUpdate;
            
            ScoreSignals.Instance.currentScore -= ReturnCurrentScore;
            ScoreSignals.Instance.totalScore -= ReturnTotalScore;

            StackSignals.Instance.onSetScoreControllerPosition -= OnSetPosition;

            CoreGameSignals.Instance.onPlay -= OnFindFollowTarget;
        }

        #endregion

        private void Update()
        {
            if (_target == null) return;
            transform.position = new Vector3(_target.position.x, _target.position.y + followOffset.y,
                _target.position.z + followOffset.z);
        }

        private void OnFindFollowTarget()
        {
            try
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                _target = player.transform;
                _currentScore = 0;
                _scoreText.text = _currentScore.ToString();
            }
            catch
            {
                Debug.Log("Player Cant find" , _target);
            }
        }



        private void OnCurrentLevelScoreUpdate(int score)
        {
            _currentScore += score;
            _scoreText.text = _currentScore.ToString();
            SaveScoreParams();
        }

        private void OnTotalScoreUpdate(int score)
        {
            _totalScore += score;
            _scoreText.text = _currentScore.ToString();
            SaveScoreParams();
        }

        private void OnSetPosition(Transform _transform)
        {
            _target = _transform;
        }

        private void SaveScoreParams()
        {
            //conver ejs version
            new ScoreParams() {currentLevelScore = _currentScore, totalScore = _totalScore };
        }
        
        private int ReturnCurrentScore() {return _currentScore; }
        
        private int ReturnTotalScore() {return _totalScore; }
    }
}