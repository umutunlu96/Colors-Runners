using Commands;
using DG.Tweening;
using Enums;
using Signals;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityObject;
using ValueObject;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialize Variables

        [SerializeField] private List<Transform> _collectable = new List<Transform>();
        [SerializeField] private List<Transform> _tempList = new List<Transform>();

        [SerializeField] private ColorType colorType;
        [SerializeField] private GameObject stickmanPrefab;

        #endregion Serialize Variables

        #region private Variables

        private AddCollectablesAfterDroneAnimationDoneCommand _addCollectablesAfterDroneAnimationDoneCommand;
        private AddStackCommand _addStackCommand;
        private LerpData _lerpData;
        private Transform _playerPossition;
        private RemoveStackCommand _removeStackCommand;
        private ShakeStackCommand _shakeStakeCommand;
        private StackEnterDroneAreaCommand _stackEnterDroneAreaCommand;
        private StackLerpMoveCommand _stackLerpMoveCommand;

        #endregion private Variables

        #endregion Self Variables

        private void Awake()
        {
            _playerPossition = GameObject.FindGameObjectWithTag("Player").transform;
            _lerpData = GetLerpData();
            _shakeStakeCommand = new ShakeStackCommand(ref _collectable, ref _lerpData);
            _addStackCommand = new AddStackCommand(ref _collectable, ref _shakeStakeCommand, transform, this);
            _removeStackCommand = new RemoveStackCommand(ref _collectable);
            _stackLerpMoveCommand = new StackLerpMoveCommand(ref _collectable, ref _lerpData, _playerPossition);
            _stackEnterDroneAreaCommand = new StackEnterDroneAreaCommand(ref _collectable, ref _tempList);
            _addCollectablesAfterDroneAnimationDoneCommand = new AddCollectablesAfterDroneAnimationDoneCommand(ref _collectable, ref _tempList);
            OnInitializeStackOnStart(6);
        }

        private void FixedUpdate()
        {
            _stackLerpMoveCommand.OnLerpStackMove();
        }

        private LerpData GetLerpData() => Resources.Load<CD_Lerp>("Data/CD_Lerp").Data;

        #region Event Subscriptions

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            StackSignals.Instance.onAddStack += _addStackCommand.OnAddStack;
            StackSignals.Instance.onRemoveFromStack += _removeStackCommand.OnRemoveFromStack;
            StackSignals.Instance.onLerpStack += _stackLerpMoveCommand.OnLerpStackMove;
            StackSignals.Instance.onSetStackStartSize += OnInitializeStackOnStart;
            //StackSignals.Instance.onThrowStackInMiniGame += OnThrowStackInMiniGame;
            StackSignals.Instance.onStackEnterDroneArea += _stackEnterDroneAreaCommand.OnStackEnterDroneArea;
            StackSignals.Instance.onMergeToPLayer += OnMergeToPLayer;
            StackSignals.Instance.onAddAfterDroneAnimationDone += _addCollectablesAfterDroneAnimationDoneCommand.OnAddCollectablesAfterDroneAnimationDone;
            StackSignals.Instance.onGetFirstCollectable += OnGetFirstCollectable;
            PlayerSignals.Instance.onChangeAllCollectableColorType += OnChangeAllCollectableColorType;
        }

        private void UnSubscribe()
        {
            StackSignals.Instance.onAddStack -= _addStackCommand.OnAddStack;
            StackSignals.Instance.onRemoveFromStack -= _removeStackCommand.OnRemoveFromStack;
            StackSignals.Instance.onLerpStack -= _stackLerpMoveCommand.OnLerpStackMove;
            StackSignals.Instance.onSetStackStartSize -= OnInitializeStackOnStart;
            //StackSignals.Instance.onThrowStackInMiniGame -= OnThrowStackInMiniGame;
            StackSignals.Instance.onStackEnterDroneArea -= _stackEnterDroneAreaCommand.OnStackEnterDroneArea;
            StackSignals.Instance.onMergeToPLayer -= OnMergeToPLayer;
            StackSignals.Instance.onAddAfterDroneAnimationDone -= _addCollectablesAfterDroneAnimationDoneCommand.OnAddCollectablesAfterDroneAnimationDone;
            StackSignals.Instance.onGetFirstCollectable += OnGetFirstCollectable;
            PlayerSignals.Instance.onChangeAllCollectableColorType -= OnChangeAllCollectableColorType;
        }
        #endregion Event Subscriptions

        private void OnChangeAllCollectableColorType(ColorType type)
        {
            StackSignals.Instance.onChangeMatarialColor?.Invoke(type);
        }

        private Transform OnGetFirstCollectable()
        {
            if (_collectable == null) return null;
            return _collectable[0];
        }

        private void OnInitializeStackOnStart(int size)
        {
            GameObject firstInitialStack = Instantiate(stickmanPrefab, _playerPossition);
            _collectable.Add(firstInitialStack.transform);
            firstInitialStack.transform.SetParent(transform);
            ScoreSignals.Instance.onCurrentLevelScoreUpdate?.Invoke();

            for (int i = 0; i < size; i++)
            {
                GameObject stackInstance = Instantiate(stickmanPrefab, _collectable.Last());
                stackInstance.transform.SetParent(transform);
                _collectable.Add(stackInstance.transform);
                ScoreSignals.Instance.onCurrentLevelScoreUpdate?.Invoke();
            }

            StackSignals.Instance.onSetScoreControllerPosition?.Invoke(_collectable[0]);
            _collectable.TrimExcess();

            OnChangeAllCollectableColorType(colorType);

            ScoreSignals.Instance.onHideScore?.Invoke();
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
                    _playerPossition.DOScale(
                        new Vector3(_playerPossition.localScale.x + 0.0375f, _playerPossition.localScale.y + 0.0375f,
                            _playerPossition.localScale.z + 0.0375f), 0.3f));
            }
        }
    }
}