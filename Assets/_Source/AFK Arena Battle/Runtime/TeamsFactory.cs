using UnityEngine;

public class TeamsFactory : MonoBehaviour
{
    public Team CreateTeam1()
    {
        var tank = new Unit("Tank", 10, 1000, new MeleeAttack());
        var Healer = new Unit("Healer", 1, 200, new RangeAttack(), new Heal());

        return new Team(tank, Healer);
    }

    public Team CreateTeam2()
    {
        var fighter = new Unit("Fighter", 100, 500, new MeleeAttack());

        return new Team(fighter);
    }
}
