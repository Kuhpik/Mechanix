using UnityEngine;

public class TeamsFactory : MonoBehaviour
{
    [SerializeField] private UnitView magePrefab;
    [SerializeField] private UnitView warriorPrefab;

    [Header("Factories")]
    [SerializeField] private AbilitiesFactory abilitiesFactory;

    public Team CreateTeam1()
    {
        var ability1 = new RangeAttack(0.5f, 3f, false);
        var mage = new Unit("Mage", 10, 200, ability1);
        var view = Instantiate(GetView("Mage"));

        mage.Position = Vector2.left * 5 + Vector2.down * 0.5f;
        view.Initialize(mage);

        CreateAbilityView(ability1, "RangeAttack");

        return new Team(mage);
    }

    public Team CreateTeam2()
    {
        var ability1 = new MeleeAttack(0.5f, 3f, false);
        var ability2 = new Heal(3f, 10f, false);
        var fighter = new Unit("Fighter", 20, 500, ability1, ability2);
        var view = Instantiate(GetView("Fighter"));

        fighter.Position = Vector2.right * 5 + Vector2.down * 0.5f;
        view.Initialize(fighter);

        CreateAbilityView(ability2, "Heal");

        return new Team(fighter);
    }

    private UnitView GetView(string name)
    {
        if (name == "Mage")
            return magePrefab;
        if (name == "Fighter")
            return warriorPrefab;

        return warriorPrefab;
    }

    private void CreateAbilityView(Ability model, string name)
    {
        abilitiesFactory.CreateAbility(name, model);
    }
}
