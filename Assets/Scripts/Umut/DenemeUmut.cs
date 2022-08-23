using System.Collections;
using System.Collections.Generic;
using Enums;
using StateMachine;
using Signals;
using UnityEngine;

namespace Umut
{
    public class DenemeUmut : MonoBehaviour
    {
    public void Runner()
    {
        CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Runner);
        PlayerSignals.Instance.onTranslateAnimationState?.Invoke(new RunnerAnimationState());
    }
    
    public void Idle()
    {
        CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Idle);
        PlayerSignals.Instance.onTranslateAnimationState?.Invoke(new IdleAnimationState());
    }
    
    public void Sneak()
    {
        PlayerSignals.Instance.onTranslateAnimationState?.Invoke(new SneakIdleAnimationState());
    }
    
    public void SneakWalk()
    {
        PlayerSignals.Instance.onTranslateAnimationState?.Invoke(new SneakWalkAnimationState());
    }
    
    public void Die()
    {
        PlayerSignals.Instance.onTranslateAnimationState?.Invoke(new DeathAnimationState());
    }
    
    public void Stop()
    { 
        //write player stop signals
    }
    
    public void OnPlay()
    {
        CoreGameSignals.Instance.onPlay?.Invoke();
    }
    }
}
