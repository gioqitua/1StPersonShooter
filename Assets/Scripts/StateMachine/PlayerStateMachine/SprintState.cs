using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintState : PlayerBaseState
{
    public SprintState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.moveSpeed *= stateMachine.runSpeedMultiplier;

    }

    public override void Exit()
    {
        stateMachine.moveSpeed /= stateMachine.runSpeedMultiplier; 
    }

    public override void Tick(float deltaTime)
    {
        if (!stateMachine.inputReader.runPressed || !stateMachine.IsMovingForward())
        {
            stateMachine.SwitchState(new MoveState(stateMachine));
        }

        stateMachine.SetSprintCamFOV();
    }
}
