using UnityEngine;

public class AbilitiesFactory : MonoBehaviour
{
    [SerializeField] private ParticleSystem healParticles;
    [SerializeField] private ParticleSystem fireballPartices;

    public AbilityView CreateAbility(string abilityName, Ability model)
    {
        var view = new GameObject(abilityName).AddComponent<AbilityView>();

        if (abilityName == "Heal")
            view.Initialize(healParticles, model);
        else if (abilityName == "RangeAttack")
            view.Initialize(fireballPartices, model);

        return view;
    }
}
