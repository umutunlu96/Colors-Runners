using System;
using System.Collections.Generic;
using System.Linq;
using Signals;
using DG.Tweening;
using UnityObject;
using ValueObject;
using StateMachine;
using UnityEngine;
using Commands;
using System.Collections;
using Controllers;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialize Variables

        [SerializeField] List<Transform> _collectable = new List<Transform>();
        [SerializeField] List<Transform> _tempList = new List<Transform>();

        #endregion

        #region private Variables


        private Transform _playerPossition;
        private Material _playerMat;
        private LerpData _lerpData;

        private ShakeStackCommand _shakeStakeCommand;
        private AddStackCommand _addStackCommand;
        private RemoveStackCommand _removeStackCommand;
        private StackLerpMoveCommand _stackLerpMoveCommand;

        #endregion

        #endregion

        

        //temp
        private void Awake()
        {
            _playerPossition = GameObject.FindGameObjectWithTag("Player").transform;
            _lerpData = GetLerpData();
            _shakeStakeCommand = new ShakeStackCommand(ref _collectable, ref _lerpData);
            _addStackCommand = new AddStackCommand(ref _collectable, ref _shakeStakeCommand, transform, this);
            _removeStackCommand = new RemoveStackCommand(ref _collectable);
            _stackLerpMoveCommand = new StackLerpMoveCommand(ref _collectable, ref _lerpData, _playerPossition);
        }

        private void FixedUpdate()
        {
          
            _stackLerpMoveCommand.OnLerpStackMove();
            
        }

        private LerpData GetLerpData() => Resources.Load<CD_Lerp>("Data/CD_Lerp").Data;

        #region Subscriptions

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            StackSignals.Instance.onAddStack += _addStackCommand.OnAddStack;
            StackSignals.Instance.OnRemoveFromStack += _removeStackCommand.OnRemoveFromStack;
            StackSignals.Instance.OnLerpStack += _stackLerpMoveCommand.OnLerpStackMove;
            StackSignals.Instance.OnSetStackStartSize += OnSetStackStartSize;
            StackSignals.Instance.OnThrowStackInMiniGame += OnThrowStackInMiniGame;
            StackSignals.Instance.onStackOnDronePath += OnStackOnDronePath;
        }

        private void UnSubscribe()
        {
            StackSignals.Instance.onAddStack -= _addStackCommand.OnAddStack;
            StackSignals.Instance.OnRemoveFromStack -= _removeStackCommand.OnRemoveFromStack;
            StackSignals.Instance.OnLerpStack -= _stackLerpMoveCommand.OnLerpStackMove;
            StackSignals.Instance.OnSetStackStartSize -= OnSetStackStartSize;
            StackSignals.Instance.OnThrowStackInMiniGame -= OnThrowStackInMiniGame;
            StackSignals.Instance.onStackOnDronePath -= OnStackOnDronePath;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        #endregion

        private void OnSetStackStartSize(int size) 
        {
        }


        private void OnThrowStackInMiniGame() 
        {
            
        }

        private void OnStackOnDronePath(Transform collectable, Transform mat)
        {
            if(!_collectable.Contains(collectable)) return;
            PlayerSignals.Instance.onTranslateAnimationState(new SneakWalkAnimationState());
            _tempList.Add(collectable);
            _collectable.Remove(collectable);
            _collectable.TrimExcess();
            _tempList.TrimExcess();
            collectable.DOMove(new Vector3(mat.position.x, collectable.position.y, collectable.position.z + UnityEngine.Random.Range(6, 10)), 3f)
                .OnComplete(() => PlayerSignals.Instance.onTranslateAnimationState(new SneakIdleAnimationState()));
        }

        private void OnAfterStackOnDronePath() { }
        private void SortStack()
        {
        }
    }
}
