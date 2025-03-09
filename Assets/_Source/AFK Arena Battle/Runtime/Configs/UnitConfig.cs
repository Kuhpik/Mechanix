using UnityEngine;

[CreateAssetMenu(fileName = "UnitConfig", menuName = "Scriptable Objects/UnitConfig")]
public class UnitConfig : ScriptableObject
{
    [field: SerializeField] public GameObject View { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public AbilityConfig[] Abilities { get; private set; }
    [field: SerializeField] public float Movespeed { get; private set; } = 1;
}
