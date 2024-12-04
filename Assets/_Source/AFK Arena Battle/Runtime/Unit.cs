using UnityEngine;

// TODO: Separate with FSM
public class Unit
{
    public readonly int MaxHealth;

    public Vector2 Position { get; private set; }
    public EUnitState State { get; private set; }
    public Unit Target { get; private set; }
    public int Health { get; private set; }
    public Team Team { get; private set; }
    public bool IsDead => Health <= 0;

    private readonly int damage;
    private readonly Spell spell;
    private readonly float moveSpeed = 1;
    private readonly float attackTime = 0.1f;
    private readonly float attackRange = 0.5f;
    private readonly float delayBetweenAttacks = 0.5f;

    private float attackDelayTimer;
    private float attackTimer;
    private bool hasAttacked;

    public Unit(int damage, int maxHealth, Spell spell)
    {
        this.spell = spell;
        this.damage = damage;
        Health = maxHealth;
        MaxHealth = maxHealth;

        ResetAttackTimer();
        ResetRestTimer();
    }

    public void Update(float deltaTime)
    {
        spell.Update(deltaTime);

        if (Target == null)
            return;

        if (!IsCloseToTarget())
        {
            MoveToTarget(deltaTime);
            return;
        }

        if (!hasAttacked)
        {
            PerformAttack(deltaTime);
            return;
        }

        RestAfterAttack(deltaTime);
    }

    public void SetTarget(Unit target)
    {
        Target = target;
    }

    public void SetTeam(Team team)
    {
        Team = team;
    }

    public void GetDamage(int damage)
    {
        Health = Mathf.Clamp(Health - damage, 0, MaxHealth);
    }

    private bool IsCloseToTarget()
    {
        return Vector2.Distance(Position, Target.Position) <= attackRange;
    }

    private void MoveToTarget(float deltaTime)
    {
        State = EUnitState.Move;
        var moveDirection = Target.Position - Position;
        Position += moveDirection.normalized * (moveSpeed * deltaTime);
    }

    private void PerformAttack(float deltaTime)
    {
        State = EUnitState.Attack;
        attackTimer -= deltaTime;

        if (attackTimer <= 0)
        {
            hasAttacked = true;
            ResetAttackTimer();
        }

        if (spell.CanCast)
            spell.Use();
        else
            Target.GetDamage(damage);
    }

    private void RestAfterAttack(float deltaTime)
    {
        State = EUnitState.Idle;
        attackDelayTimer -= deltaTime;

        if (attackDelayTimer <= 0)
        {
            hasAttacked = false;
            ResetRestTimer();
        }
    }

    private void ResetAttackTimer()
    {
        attackTimer = attackTime;
    }

    private void ResetRestTimer()
    {
        attackDelayTimer = delayBetweenAttacks;
    }
}
