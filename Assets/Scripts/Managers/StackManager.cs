using System;
using System.Collections.Generic;
using System.Linq;
using Signals;
using UnityObject;
using ValueObject;
using UnityEngine;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region SelfVariables

        #region privateVariables

        private List<Transform> _collectable = new List<Transform>();
        private Transform _playerPossition;
        private LerpData _lerpData;

        #endregion

        #endregion

        

        //temp
        private void Awake()
        {
            _playerPossition = GameObject.FindGameObjectWithTag("Player").transform;
            _lerpData = GetLerpData();
            Debug.Log(_lerpData.LerpSpeeds);
        }

        private void Update()
        {
            OnLerpStackMove();
        }

        private LerpData GetLerpData() => Resources.Load<CD_Lerp>("Data/CD_Lerp").Data;

        #region Subscriptions

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            StackSignals.Instance.onAddStack += OnAddStack;
            StackSignals.Instance.OnRemoveFromStack += OnRemoveFromStack;
            StackSignals.Instance.OnLerpStack += OnLerpStackMove;
            StackSignals.Instance.OnSetStackStartSize += OnSetStackStartSize;
            StackSignals.Instance.OnShakeStackSize += OnShakeStackSize;
            StackSignals.Instance.OnThrowStackInMiniGame += OnThrowStackInMiniGame;
        }

        private void UnSubscribe()
        {
            StackSignals.Instance.onAddStack -= OnAddStack;
            StackSignals.Instance.OnRemoveFromStack -= OnRemoveFromStack;
            StackSignals.Instance.OnLerpStack -= OnLerpStackMove;
            StackSignals.Instance.OnSetStackStartSize -= OnSetStackStartSize;
            StackSignals.Instance.OnShakeStackSize -= OnShakeStackSize;
            StackSignals.Instance.OnThrowStackInMiniGame -= OnThrowStackInMiniGame;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        #endregion

       
        private void OnAddStack(Transform collectable)
        {
            collectable.tag = "Collected";
            collectable.SetParent(transform);
            _collectable.Add(collectable);
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
                    Mathf.Lerp(_collectable[0].localPosition.z, _playerPossition.localPosition.z - _lerpData.DistanceOffSet, 0.5f)
                    );

                //after each stack flow each other by n flow n - 1 prenciple by give offset and time 
                for(int i = 1; i < _collectable.Count; i++)
                {
                    _collectable[i].localPosition = new Vector3(
                         Mathf.Lerp(_collectable[i].localPosition.x, _collectable[i - 1].localPosition.x, 0.5f),
                         Mathf.Lerp(_collectable[i].localPosition.y, _collectable[i - 1].localPosition.y, 0.5f),
                         Mathf.Lerp(_collectable[i].localPosition.z, _collectable[i - 1].localPosition.z - _lerpData.DistanceOffSet, 0.5f)
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
