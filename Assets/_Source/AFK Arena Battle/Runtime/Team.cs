using System.Collections.Generic;

public class Team
{
    private readonly Unit[] members;
    private readonly HashSet<Unit> membersMap;

    public Team(params Unit[] units)
    {
        members = units;
        membersMap = new HashSet<Unit>(units);

        foreach (var unit in units)
        {
            unit.SetTeam(this);
        }
    }

    public bool IsAlly(Unit unit)
    {
        return membersMap.Contains(unit);
    }

    public bool IsDefeated()
    {
        foreach (var unit in members)
        {
            if (!unit.IsDead)
                return false;
        }

        return true;
    }

    public IEnumerable<Unit> GetMembers()
    {
        return members;
    }
}
