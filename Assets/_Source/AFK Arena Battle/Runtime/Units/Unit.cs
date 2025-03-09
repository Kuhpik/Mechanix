using System;
using UnityEngine;

public interface IUnit
{ 
    float MoveSpeed { get; set; }
    Vector2 Position { get; set; }
    Vector2 MoveDirection { get; set; }
    EUnitState State { get; }
    Ability AbilityCasted { get; }
   
    void Update(float deltaTime);

    event Action OnUpdated;
}

// TODO: Separate with FSM
public class Unit : IUnit
{
    public readonly int MaxHealth;
    public readonly int Damage;
    public readonly string Name;

    public float MoveSpeed { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 MoveDirection { get; set; }

    public Ability AbilityCasted { get; private set; }
    public EUnitState State { get; private set; }
    public Unit Target { get; private set; }
    public int Health { get; private set; }
    public Team Team { get; private set; }
    public bool IsDead => Health <= 0;

    public event Action OnUpdated;

    /// <summary>
    /// Attacker, Damaged, Damage Amount
    /// </summary>
    public event Action<Unit, Unit, int> OnDamaged;

    private readonly Ability[] abilities;

    public Unit(string name, int damage, int maxHealth, params Ability[] abilities)
    {
        this.abilities = abilities;

        Name = name;
        Damage = damage;
        Health = maxHealth;
        MaxHealth = maxHealth;
        MoveSpeed = 1;
    }

    public virtual void Update(float deltaTime)
    {
        ChangeState(deltaTime);
        OnUpdated?.Invoke();
    }

    private void ChangeState(float deltaTime)
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

        MoveDirection = (Target.Position - Position).normalized;

        if (IsPerformingAttack())
        {
            State = EUnitState.Attack;
            return;
        }

        CheckIfWeCanMoveOrCast(out bool shouldMove, out Ability abilityToCast);

        if (abilityToCast != null)
        {
            abilityToCast.Cast(this);
            return;
        }

        if (shouldMove)
        {
            Move(deltaTime);
            State = EUnitState.Move;
            return;
        }

        State = EUnitState.Idle;
    }

    private void CheckIfWeCanMoveOrCast(out bool shouldMove, out Ability abilityToCast)
    {
        shouldMove = false;
        abilityToCast = null;

        foreach (var ability in abilities)
        {
            bool inRange = ability.IsTargetInRange(Position, Target.Position);

            if (inRange && ability.CanCast)
            {
                abilityToCast = ability;
                break;
            }

            if (!inRange)
            {
                shouldMove = true;
            }
        }
    }

    public void Stop()
    {
        State = EUnitState.Idle;
        OnUpdated?.Invoke();
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

    private bool IsPerformingAttack()
    {
        foreach (var ability in abilities)
        {
            if (ability.IsCasting)
                return true;
        }

        return false;
    }

    protected void Move(float deltaTime)
    {
        Position += GetMoveDistance(deltaTime);
    }

    private Vector2 GetMoveDistance(float deltaTime)
    {
        return MoveDirection * (MoveSpeed * deltaTime);
    }
}
