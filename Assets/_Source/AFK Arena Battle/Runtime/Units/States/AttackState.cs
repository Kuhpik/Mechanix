using UnityEngine;

public class AttackState : MonoBehaviour
{
    private IUnit unit;
    private Animator animator;
    private float animationTime;
    private float animationTimePassed;

    public void OnEnter()
    {
        animationTimePassed = 0;

        var animationTime = unit.AbilityCasted.CastTime;
        var animatorState = animator.GetCurrentAnimatorStateInfo(0);
    }

    public void Run(float deltaTime)
    {
        if (animationTimePassed >= animationTime)
        {
            //ExitState();
        }

        animationTimePassed += deltaTime;
    }

    public void OnExit()
    { 
    
    }
}
