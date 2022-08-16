namespace StateMachine
{
    public class CameraIdleState : CameraStateMachine
    {
        public override void ChangeStateCamera()
        {
            _runnerCamera.Follow = _target;
            _cinamationAnimationStates.Play("IdleCam");
        }
    }
}
