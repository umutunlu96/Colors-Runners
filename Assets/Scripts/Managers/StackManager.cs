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
using Enums;
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

        [SerializeField] private ColorType colorType;
        [SerializeField] private GameObject stickmanPrefab; 
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

        private void Awake()
        {
            _playerPossition = GameObject.FindGameObjectWithTag("Player").transform;    //OnPlaye cekileck
            _lerpData = GetLerpData();
            _shakeStakeCommand = new ShakeStackCommand(ref _collectable, ref _lerpData);
            _addStackCommand = new AddStackCommand(ref _collectable, ref _shakeStakeCommand, transform, this);
            _removeStackCommand = new RemoveStackCommand(ref _collectable);
            _stackLerpMoveCommand = new StackLerpMoveCommand(ref _collectable, ref _lerpData, _playerPossition);
            OnSetStackStartSize(6);
        }
        
        private LerpData GetLerpData() => Resources.Load<CD_Lerp>("Data/CD_Lerp").Data;

        #region Event Subscriptions

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            StackSignals.Instance.onAddStack += _addStackCommand.OnAddStack;
            StackSignals.Instance.onRemoveFromStack += _removeStackCommand.OnRemoveFromStack;
            StackSignals.Instance.onLerpStack += _stackLerpMoveCommand.OnLerpStackMove;
            StackSignals.Instance.onSetStackStartSize += OnSetStackStartSize;
            //StackSignals.Instance.onThrowStackInMiniGame += OnThrowStackInMiniGame;
            StackSignals.Instance.onStackEnterDroneArea += OnStackEnterDroneArea;
            StackSignals.Instance.onMergeToPLayer += OnMergeToPLayer;
            StackSignals.Instance.onAddAfterDroneAnimationDone += OnAddAfterDroneAnimationDone;
            PlayerSignals.Instance.onChangeAllCollectableColorType += OnChangeAllCollectableColorType;
        }

        private void UnSubscribe()
        {
            StackSignals.Instance.onAddStack -= _addStackCommand.OnAddStack;
            StackSignals.Instance.onRemoveFromStack -= _removeStackCommand.OnRemoveFromStack;
            StackSignals.Instance.onLerpStack -= _stackLerpMoveCommand.OnLerpStackMove;
            StackSignals.Instance.onSetStackStartSize -= OnSetStackStartSize;
            //StackSignals.Instance.onThrowStackInMiniGame -= OnThrowStackInMiniGame;
            StackSignals.Instance.onStackEnterDroneArea -= OnStackEnterDroneArea;
            StackSignals.Instance.onMergeToPLayer -= OnMergeToPLayer;
            StackSignals.Instance.onAddAfterDroneAnimationDone -= OnAddAfterDroneAnimationDone;
            PlayerSignals.Instance.onChangeAllCollectableColorType -= OnChangeAllCollectableColorType;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        #endregion

        
        private void FixedUpdate()
        {
            _stackLerpMoveCommand.OnLerpStackMove();
        }

        
        private void OnChangeAllCollectableColorType(ColorType type)
        {
            foreach (var item in _collectable)
            {
                item.GetComponent<CollectableManager>().ChangeMaterialColor(type); //Command
            }
        }

        private void OnStackEnterDroneArea(Transform collectable, Transform mat)
        {
            if (!_collectable.Contains(collectable)) return;
            PlayerSignals.Instance.onTranslateAnimationState?.Invoke(new SneakWalkAnimationState());
            _tempList.Add(collectable);
            _collectable.Remove(collectable);
            _collectable.TrimExcess();
            _tempList.TrimExcess();
            var position = collectable.position;
            collectable.DOMove(
                    new Vector3(mat.position.x, position.y,
                        position.z + UnityEngine.Random.Range(6, 10)), 1.5f)
                .OnComplete(() => PlayerSignals.Instance.onTranslateAnimationState(new SneakIdleAnimationState()));

            if (_collectable.Count == 0)
            {
                StackSignals.Instance.onLastCollectableEnterDroneArea?.Invoke();
            }
        }

        private void OnMergeToPLayer()
        {
            _tempList = _collectable;
            _collectable.Clear();
            _tempList.TrimExcess();
            _collectable.TrimExcess();

            foreach (var stack in _tempList)
            {
                stack.DOMoveZ(_playerPossition.position.z, 0.4f).OnComplete(() =>
                {
                    Vector3 localScale;
                    _playerPossition.DOScale(
                        new Vector3((localScale = _playerPossition.localScale).x + 0.0375f, localScale.y + 0.0375f,
                            localScale.z + 0.0375f), 0.3f);
                });
            }
        }

        private void OnAddAfterDroneAnimationDone(bool isDead, Transform collectableTranform)
        {
            switch (isDead)
            {
                case true:
                    _tempList.Remove(collectableTranform);
                    _tempList.TrimExcess();
                    break;
                case false when _tempList.Contains(collectableTranform):
                    _tempList.Remove(collectableTranform);
                    _collectable.Add(collectableTranform);
                    _tempList.TrimExcess();
                    _collectable.TrimExcess();
                    StackSignals.Instance.onSetScoreControllerPosition?.Invoke(_collectable[0]);
                    break;
            }
        }

        private void OnSetStackStartSize(int size)
        {
            for (int i = 0; i < size; i++)
            {
                _addStackCommand.OnAddStack(Instantiate(stickmanPrefab).transform);                
            }

            for (int i = 0; i < size; i++)
            {
                _collectable[i].GetComponent<CollectableManager>().ChangeMaterialColor(colorType);
            }
        }
    }
}