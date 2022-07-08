using Photon.Pun;
using UnityEngine;

public abstract class StateMachine : MonoBehaviourPunCallbacks
{
    internal State currentState;
    private void Update()
    {
        if (!photonView.IsMine) return;
        currentState.Tick(Time.deltaTime);
    }
    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}
