 
public class AttackState : EnemyBaseState
{

    public AttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.anim.SetTrigger("Attack");
        stateMachine.attackComplete += TryDealDamage;
    }

    public override void Exit()
    {
        stateMachine.attackComplete -= TryDealDamage;
    }

    private void TryDealDamage()
    {
        if (stateMachine.CalculateDistance() > stateMachine.attackRange)
        {
            stateMachine.SwitchState(new ChasingState(stateMachine));
        }
        else
        {
            GameManager.Instance.PlayerGetHit(stateMachine.damagePerHit);
            stateMachine.anim.SetTrigger("Attack");
        }
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.navmeshAgent.SetDestination(stateMachine.playerPos.position);
    }

}
