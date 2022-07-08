using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : PlayerBaseState
{
    public AimState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.moveSpeed *= stateMachine.aimSpeedMultiplier;
        stateMachine.weaponSystem.isAiming = true;        
    }

    public override void Exit()
    {
        stateMachine.moveSpeed /= stateMachine.aimSpeedMultiplier;
        stateMachine.weaponSystem.isAiming = false;

    }

    public override void Tick(float deltaTime)
    {
        stateMachine.Shoot();
        stateMachine.SetAimCamFOV();
        
        if (!stateMachine.inputReader.aimPressed)
        {
            stateMachine.SwitchState(new MoveState(stateMachine));
        }
        if (stateMachine.inputReader.runPressed)
        {
            stateMachine.SwitchState(new SprintState(stateMachine));
        }
    }
}
