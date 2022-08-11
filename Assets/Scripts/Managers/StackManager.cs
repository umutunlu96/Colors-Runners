using System;
using System.Collections.Generic;
using System.Linq;
using Signals;
using UnityEngine;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region SelfVariables

        #region privateVariables

        private List<Transform> _collectable = new List<GameObject>();
        private Transform _playerPossition;

        #endregion

        #endregion

        #region Subscriptions

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            StackSignals.Instance.onAddStack += OnAddStack;
            StackSignals.Instance.OnRemoveFromStack += OnRemoveFromStack;
            StackSignals.Instance.OnLerpStack += OnLerpStack;
            StackSignals.Instance.OnSetStackStartSize += OnSetStackStartSize;
            StackSignals.Instance.OnShakeStackSize += OnShakeStackSize;
            StackSignals.Instance.OnThrowStackInMiniGame += OnThrowStackInMiniGame;
        }

        private void UnSubscribe()
        {
            StackSignals.Instance.onAddStack -= OnAddStack;
            StackSignals.Instance.OnRemoveFromStack -= OnRemoveFromStack;
            StackSignals.Instance.OnLerpStack -= OnLerpStack;
            StackSignals.Instance.OnSetStackStartSize -= OnSetStackStartSize;
            StackSignals.Instance.OnShakeStackSize -= OnShakeStackSize;
            StackSignals.Instance.OnThrowStackInMiniGame -= OnThrowStackInMiniGame;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        #endregion

        //temp
        private void Awake()
        {
            _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void OnAddStack(GameObject _gameObject)
        {

        }

        private void OnRemoveFromStack(int index)
        {

        }

        private void OnSetStackStartSize(int size) 
        {
        }

        private void OnLerpStackMove() 
        {
            if(_collectable.Count > 0)
            {
                //put pack to stack behind the player
                _collectable[0].localPosition = new Vector3(
                    Mathf.Lerp(_collectable[0].localPosition.x, _playerPossition.localPosition.x, 0.5f),
                    Mathf.Lerp(_collectable[0].localPosition.y, _playerPossition.localPosition.y, 0.5f),
                    Mathf.Lerp(_collectable[0].localPosition.z, _playerPossition.localPosition.z - 2, 0.5f
                    );

                //after each stack flow each other by n flow n - 1 prenciple by give offset and time 
                for(int i = 1; i < _collectable.Count; i++)
                {
                    _collectable[i].localPosition = new Vector3(
                         Mathf.Lerp(_collectable[i].localPosition.x, _collectable[i - 1].localPosition.x, 0.5f),
                         Mathf.Lerp(_collectable[i].localPosition.y, _collectable[i - 1].localPosition.x, 0.5f),
                         Mathf.Lerp(_collectable[i].localPosition.z, _collectable[i - 1].localPosition.x - 2, 0.5f)
                         );

                }

            }
        }

        private void OnShakeStackSize() 
        {
        }

        private void OnThrowStackInMiniGame() 
        {
        }

        private void SortStack()
        {
        }
    }
}
