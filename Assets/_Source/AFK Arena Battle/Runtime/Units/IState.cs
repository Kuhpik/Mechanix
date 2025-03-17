public interface IState
{
    void Update(float deltaTime);
    void Enter();
    void Exit();
}
