using NUnit.Framework;
using UnityEngine;

public class UnitAbilitiesTests
{
    [Test]
    public void Only_One_Ability_Can_Be_Casted_At_A_Time()
    {
        var ability1 = new TestAbility(castTime: 2, cooldown: 10, isAvailableAtTheStart: true);
        var ability2 = new TestAbility(castTime: 2, cooldown: 10, isAvailableAtTheStart: true);
    }
}
