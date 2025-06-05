using UnityEngine;
using NaughtyAttributes;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField, AnimatorParam(nameof(animator))] private string idleTrigger;
    [SerializeField, AnimatorParam(nameof(animator))] private string walkTrigger;
    [SerializeField, AnimatorParam(nameof(animator))] private string attackTrigger;
    [SerializeField, AnimatorParam(nameof(animator))] private string deadTrigger;
    [SerializeField, AnimatorParam(nameof(animator))] private string castTrigger;
    [SerializeField, AnimatorParam(nameof(animator))] private string victoryTrigger;

    private string[] allTriggers;

    private void Awake()
    {
        allTriggers = new string[] { idleTrigger, walkTrigger, attackTrigger, deadTrigger, castTrigger, victoryTrigger };
    }

    public void Animate(EUnitState state)
    {
        var activeTrigger = GetAnimatorParam(state);

        foreach (var trigger in allTriggers)
        {
            if (trigger == activeTrigger) animator.SetTrigger(activeTrigger);
            else animator.ResetTrigger(trigger);
        }
    }

    private string GetAnimatorParam(EUnitState state) => state switch
    {
        EUnitState.Idle => idleTrigger,
        EUnitState.Move => walkTrigger,
        EUnitState.Attack => attackTrigger,
        EUnitState.Cast => castTrigger,
        EUnitState.Dead => deadTrigger,
        EUnitState.Victory => victoryTrigger,
        _ => "",
    };

}
