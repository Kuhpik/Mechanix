using UnityEngine;
using NaughtyAttributes;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField, AnimatorParam(nameof(animator))] private string walkTrigger;
    [SerializeField, AnimatorParam(nameof(animator))] private string attackTrigger;
    [SerializeField, AnimatorParam(nameof(animator))] private string deadTrigger;
}
