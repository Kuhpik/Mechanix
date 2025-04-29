using System;
using UnityEngine;

public interface IUnit
{
    string Name { get; }

    int Damage { get; }
    int Health { get; }
    int MaxHealth { get; }

    Team Team { get; }
    Unit Target { get; }
    EUnitState State { get; }
    Ability AbilityCasted { get; }

    float MoveSpeed { get; set; }
    Vector2 Position { get; set; }
    Vector2 MoveDirection { get; set; }

    void Update(float deltaTime);

    event Action OnUpdated;
}

// TODO: Separate with FSM
public class Unit : IUnit
{
    public int MaxHealth { get; private set; }
    public int Damage { get; private set; }
    public string Name { get; private set; }

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
    public event Action<IUnit, IUnit, int> OnDamaged;

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
        if (State == EUnitState.Dead || State == EUnitState.Victory)
        {
            return;
        }

        if (IsDead)
        {
            SetState(EUnitState.Dead);
            return;
        }

        foreach (var ability in abilities)
        {
            ability.Update(deltaTime);
        }

        if (Target == null)
        {
            SetState(EUnitState.Idle);
            return;
        }

        if (IsPerformingAttack(out var abilityIndex))
        {
            SetState(abilityIndex == 0 ? EUnitState.Attack : EUnitState.Cast);
            return;
        }

        MoveDirection = (Target.Position - Position).normalized;

        CheckIfWeCanMoveOrCast(out bool shouldMove, out Ability abilityToCast);

        if (abilityToCast != null)
        {
            AbilityCasted = abilityToCast;
            abilityToCast.Cast(this);
            SetState(EUnitState.Attack);
            return;
        }

        if (AbilityCasted != null && AbilityCasted.IsCasting)
        {
            return;
        }

        else
        {
            AbilityCasted = null;
        }

        if (shouldMove)
        {
            Move(deltaTime);
            SetState(EUnitState.Move);
            return;
        }

        SetState(EUnitState.Idle);
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

    public void Victory()
    {
        SetState(EUnitState.Victory);
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

    public void ApplyDamage(IUnit attacker, int damage)
    {
        Health = Mathf.Clamp(Health - damage, 0, MaxHealth);
        OnDamaged?.Invoke(attacker, this, damage);
    }

    private bool IsPerformingAttack(out int abilityIndex)
    {
        abilityIndex = 0;

        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i].IsCasting)
            {
                abilityIndex = i;
                return true;
            }
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

    private void SetState(EUnitState nextState)
    {
        Debug.Log($"New state for Unit : {Name} is {nextState}");
        State = nextState;
    }
}
