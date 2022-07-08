 
public class ChasingState : EnemyBaseState
{
    public ChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.anim.SetBool("Run", true); 
        stateMachine.navmeshAgent.ResetPath();
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.navmeshAgent.SetDestination(stateMachine.playerPos.position);

        if (stateMachine.CalculateDistance() >= stateMachine.lookRadius)
        {
            stateMachine.SwitchState(new RoamingState(stateMachine));
        }
        if (stateMachine.CalculateDistance() <= stateMachine.attackRange)
        {
            stateMachine.SwitchState(new AttackState(stateMachine));
        }
    }

    public override void Exit()
    { 
        stateMachine.anim.SetBool("Run", false);
        stateMachine.navmeshAgent.ResetPath();
    }

}
