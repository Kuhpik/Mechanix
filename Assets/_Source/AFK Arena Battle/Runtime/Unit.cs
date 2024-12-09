using System;
using UnityEngine;

// TODO: Separate with FSM
public class Unit
{
    public readonly int MaxHealth;
    public readonly int Damage;
    public readonly string Name;

    public Vector2 Position { get; private set; }
    public EUnitState State { get; private set; }
    public Unit Target { get; private set; }
    public int Health { get; private set; }
    public Team Team { get; private set; }
    public bool IsDead => Health <= 0;

    /// <summary>
    /// Attacker, Damaged, Damage Amount
    /// </summary>
    public event Action<Unit, Unit, int> OnDamaged;

    private readonly Ability[] abilities;
    private readonly float moveSpeed = 1;

    public Unit(string name, int damage, int maxHealth, params Ability[] abilities)
    {
        this.abilities = abilities;

        Name = name;
        Damage = damage;
        Health = maxHealth;
        MaxHealth = maxHealth;
    }

    public void Update(float deltaTime)
    {
        if (IsDead)
        {
            State = EUnitState.Dead;
            return;
        }

        foreach (var ability in abilities)
        {
            ability.Update(deltaTime);
        }

        if (Target == null)
        {
            State = EUnitState.Idle;
            return;
        }

        if (IsPerformingAttack())
        {
            State = EUnitState.Attack;
            return;
        }

        if (IsHaveAbilityToCast(out var abilityToCast))
        {
            abilityToCast.Cast(this);
            return;
        }

        if (!IsTargetInRange())
        {
            MoveToTarget(deltaTime);
            State = EUnitState.Move;
            return;
        }

        State = EUnitState.Idle;
    }

    public void SetTarget(Unit target)
    {
        Target = target;
    }

    public void SetTeam(Team team)
    {
        Team = team;
    }

    public void ApplyHeal(int heal)
    {
        Health = Mathf.Clamp(Health + heal, 0, MaxHealth);
    }

    public void ApplyDamage(Unit attacker, int damage)
    {
        OnDamaged?.Invoke(attacker, this, damage);
        Health = Mathf.Clamp(Health - damage, 0, MaxHealth);
    }

    private bool IsTargetInRange()
    {
        var distance = Vector2.Distance(Target.Position, Position);
        // TODO: Optimize with Melee \ Range definitions of ability range.
        // For ex. if every spell is ranged we no longer have to perform this check.

        foreach (var ability in abilities)
        {
            if (distance > ability.Range)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsPerformingAttack()
    {
        foreach (var ability in abilities)
        {
            if (ability.IsCasting)
                return true;
        }

        return false;
    }

    private bool IsHaveAbilityToCast(out Ability abilityToCast)
    {
        abilityToCast = null;

        foreach (var ability in abilities)
        {
            if (ability.CanCast)
            {
                abilityToCast = ability;
                return true;
            }
        }

        return false;
    }

    private void MoveToTarget(float deltaTime)
    {
        var moveDirection = Target.Position - Position;
        Position += moveDirection.normalized * (moveSpeed * deltaTime);
    }
}
