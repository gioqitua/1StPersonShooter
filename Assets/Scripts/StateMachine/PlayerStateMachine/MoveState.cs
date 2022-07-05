 

public class MoveState : PlayerBaseState
{
    public MoveState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.inputReader.RunEvent += Run;
        stateMachine.inputReader.FireEvent += Fire;
    }
    void Fire()
    {
        stateMachine.Shoot();
    }
    private void Run()
    {
        stateMachine.moveSpeed *= 1.5f;
    }

    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {
        stateMachine.inputReader.RunEvent -= Run;
        stateMachine.inputReader.FireEvent -= Fire;
    }

}
