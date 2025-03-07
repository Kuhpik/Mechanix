using UnityEngine;

public class TeamsFactory : MonoBehaviour
{
    [SerializeField] private UnitView mageView;
    
    public Team CreateTeam1()
    {
        var mage = new Unit("Mage", 10, 200, new RangeAttack());
        mage.Position = Vector2.left * 5;

        var view = Instantiate(GetView("Mage"));
        view.Initialize(mage);

        return new Team(mage);
    }

    public Team CreateTeam2()
    {
        var fighter = new Unit("Fighter", 20, 100, new MeleeAttack());
        fighter.Position = Vector2.right * 5;

        var view = Instantiate(GetView("Fighter"));
        view.Initialize(fighter);

        return new Team(fighter);
    }

    private UnitView GetView(string name)
    {
        return mageView;
    }
}
