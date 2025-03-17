public class AttackState : IState
{
    private IUnit unit;
    private Ability ability;
    private StateMachine fsm;
    private float animationTime;
    private float animationTimePassed;

    public AttackState(IUnit unit, StateMachine fsm, Ability ability)
    {
        this.unit = unit;
        this.fsm = fsm;
        this.ability = ability;
    }

    public void Enter()
    {
        animationTimePassed = 0;
        var animationTime = unit.AbilityCasted.CastTime;
    }

    public void Exit()
    {
        fsm.ChangeState(EUnitState.Idle);
    }

    public void Update(float deltaTime)
    {
        if (animationTimePassed >= animationTime)
        {
            Exit();
        }

        animationTimePassed += deltaTime;
    }
}
