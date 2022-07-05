 
public class RoamingState : EnemyBaseState
{
    public RoamingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.anim.SetBool("Run", false);
    }

    public override void Tick(float deltaTime)
    {
        var playerPos = stateMachine.playerPos;
        if (stateMachine.CalculateDistance() <= stateMachine.chasingRadius)
        {
            stateMachine.SwitchState(new ChasingState(stateMachine));
        }
    }
    public override void Exit()
    {
        
    }

}
