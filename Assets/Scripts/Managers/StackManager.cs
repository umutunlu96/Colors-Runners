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

        private List<GameObject> _Stickmans = new List<GameObject>();

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

        private void OnAddStack(GameObject _gameObject)
        {

        }

        private void OnRemoveFromStack(int index)
        {

        }

        private void OnSetStackStartSize(int size) 
        {
        }

        private void OnLerpStack() 
        {
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
