
using UnityEngine;

public class RoamingState : EnemyBaseState
{
    public RoamingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.anim.SetBool("Run", false);
        stateMachine.navmeshAgent.speed = 1.5f;
    }

    public override void Tick(float deltaTime)
    {
        var playerPos = stateMachine.playerPos;

        if (stateMachine.CalculateDistance() <= stateMachine.chasingRadius)
        {
            stateMachine.SwitchState(new ChasingState(stateMachine));
        }

        if (!stateMachine.navmeshAgent.hasPath)
        {
            var x = UnityEngine.Random.Range(-15f, 15f);
            var z = UnityEngine.Random.Range(-15f, 15f);

            stateMachine.navmeshAgent.SetDestination(new Vector3(stateMachine.transform.position.x + x, 0f,
            stateMachine.transform.position.z + z));
        }


    }
    public override void Exit()
    {
        stateMachine.navmeshAgent.ResetPath();
        stateMachine.navmeshAgent.speed = 3.5f;
    }

}
