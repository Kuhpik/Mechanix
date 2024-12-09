using System.Collections.Generic;
using UnityEngine;

public class BattleLogger
{
    private Dictionary<Unit, int> unitsDamageDealt = new();

    public BattleLogger(Unit[] units)
    {
        foreach (var unit in units)
        {
            unit.OnDamaged += RecordDamageDealt;
            unitsDamageDealt.Add(unit, 0);
        }
    }

    private void RecordDamageDealt(Unit attacker, Unit damaged, int damage)
    {
        unitsDamageDealt[attacker] += damage;
        Debug.Log($"{attacker.Name} dealt {damage} damage to {damaged.Name}. HP now {damaged.Health}/{damaged.MaxHealth}");
    }
}
