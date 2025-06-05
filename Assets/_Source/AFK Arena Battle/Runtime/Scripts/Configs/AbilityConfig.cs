using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityConfig", menuName = "Scriptable Objects/AbilityConfig")]
public class AbilityConfig : ScriptableObject
{
    [field: SerializeField] public AnimationClip Animation { get; private set; }
    [field: SerializeField] public float Cooldown { get; private set; }
    [field: SerializeField, HideIf("HasMaximumRange")] public float Range { get; private set; }
    [field: SerializeField] public bool HasMaximumRange { get; private set; }
    [field: SerializeField] public bool IsAvailableAtStart { get; private set; }

    [Header("Can be empty")]
    [field: SerializeField] public GameObject Particles { get; private set; }
}
