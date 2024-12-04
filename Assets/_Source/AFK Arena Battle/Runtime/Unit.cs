public class Unit
{
    public readonly int Damage;
    public readonly int MaxHealth;
    public readonly Spell Spell;

    public readonly float MoveSpeed = 1;
    public readonly float AttackTime = 0.1f;
    public readonly float DelayBetweenAttacks = 0.5f;

    public int Health { get; private set; }
    public Team Team { get; private set; }
    public bool IsDead => Health <= 0;

    public Unit(int damage, int maxHealth, Spell spell)
    {
        Spell = spell;
        Damage = damage;
        Health = maxHealth;
        MaxHealth = maxHealth;
    }

    public void SetTeam(Team team)
    {
        Team = team;
    }
}
