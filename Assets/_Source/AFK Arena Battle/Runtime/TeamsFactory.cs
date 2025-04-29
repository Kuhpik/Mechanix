using UnityEngine;

public class TeamsFactory : MonoBehaviour
{
    [SerializeField] private UnitView mageView;
    
    public Team CreateTeam1()
    {
        var ability1 = new RangeAttack(0.5f, 3f, false);
        var mage = new Unit("Mage", 10, 200, ability1);
        var view = Instantiate(GetView("Mage"));

        mage.Position = Vector2.left * 5;
        view.Initialize(mage);

        return new Team(mage);
    }

    public Team CreateTeam2()
    {
        var ability1 = new MeleeAttack(0.5f, 3f, false);
        var ability2 = new Heal(1f, 10f, false);
        var fighter = new Unit("Fighter", 20, 500, ability1, ability2);
        var view = Instantiate(GetView("Fighter"));

        fighter.Position = Vector2.right * 5;
        view.Initialize(fighter);

        return new Team(fighter);
    }

    private UnitView GetView(string name)
    {
        return mageView;
    }
}
