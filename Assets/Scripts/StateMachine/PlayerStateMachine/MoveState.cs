

using UnityEngine;

public class MoveState : PlayerBaseState
{
    public MoveState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
        stateMachine.Shoot();

        if (stateMachine.inputReader.runPressed && stateMachine.IsMovingForward())
        {
            stateMachine.SwitchState(new SprintState(stateMachine));
        }
        if (stateMachine.inputReader.aimPressed)
        {
            stateMachine.SwitchState(new AimState(stateMachine));
        }
        stateMachine.SetNormalCamFOV();
    }
    public override void Exit()
    {

    }

}
