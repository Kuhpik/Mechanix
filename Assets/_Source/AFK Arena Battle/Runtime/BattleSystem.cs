using System;
using System.Linq;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private TeamsFactory teamsFactory;

    public Team[] Teams { get; private set; }
    public event Action OnBattleOver;

    private BattleLogger battleLogger;
    private  Unit[] units;

    private void Start()
    {
        var team1 = teamsFactory.CreateTeam1();
        var team2 = teamsFactory.CreateTeam2();

        Teams = new Team[] { team1, team2 };
        units = Teams.SelectMany(x => x.GetMembers()).ToArray();
        battleLogger = new BattleLogger(units);
    }

    private void Update()
    {
        if (IsBattleOver())
        {
            Debug.Log("Battle over!");
            enabled = false;
            return;
        }

        var deltaTime = Time.deltaTime;

        foreach (var unit in units)
        {
            TrySetNewTarget(unit);
            unit.Update(deltaTime);
        }
    }

    private void TrySetNewTarget(Unit unit)
    {
        if (unit.Target != null)
            return;

        var enemyTeam = Teams.First(x => x != unit.Team);
        // TODO: Change to smth real like targeting by places and frontlane prio
        var closestTarget = enemyTeam.GetMembers().First();

        unit.SetTarget(closestTarget);
    }

    private bool IsBattleOver()
    {
        foreach (var team in Teams)
        {
            if (team.IsDefeated())
                return true;
        }

        return false;
    }
}
