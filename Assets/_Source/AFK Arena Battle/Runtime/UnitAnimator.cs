using UnityEngine;
using NaughtyAttributes;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField, AnimatorParam(nameof(animator))] private string idleTrigger;
    [SerializeField, AnimatorParam(nameof(animator))] private string walkTrigger;
    [SerializeField, AnimatorParam(nameof(animator))] private string attackTrigger;
    [SerializeField, AnimatorParam(nameof(animator))] private string deadTrigger;

    public void Animate(EUnitState state)
    {
        var param = GetAnimatorParam(state);
        animator.SetTrigger(param);
    }

    private string GetAnimatorParam(EUnitState state) => state switch
    {
        EUnitState.Idle => idleTrigger,
        EUnitState.Move => walkTrigger,
        EUnitState.Attack => attackTrigger,
        EUnitState.Dead => deadTrigger,
        _ => "",
    };

}
